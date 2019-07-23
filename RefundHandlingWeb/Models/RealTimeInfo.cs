using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace RefundHandlingWeb.Models
{
    public class RealTimeInfo
    {
        public Guid RefundId { get; set; }
        [Display(Name = "Coupon Status Check 1A", Description = "Coupon Status Check in Amadeus.")]
        public string CouponStatusCheck1A {get;set;}
        [Display(Name = "Coupon Status Check Rapid", Description = "Coupon Status Check in Rapid.")]
        public string CouponStatusCheckRapid { get; set; }
        [Display(Name = "CRM Agency Status", Description = "Is the agency status meanwhile default?.")]
        public string CRMAgencyStatus {get;set;}
        [Display(Name = "Ticket in CRM", Description = "Which object do we have in CRM related to the ticket.")]
        public string TicketInCRM {get;set;}
        public bool CouponStatusCheck1Acolor { get; set; }
        public bool CouponStatusCheckRapidcolor { get; set; }
        public bool CRMAgencyStatuscolor { get; set; }
        public string Tax_NonRefund { get; set; }
        public string Tax_Sales { get; set; }
        public static RealTimeInfo getRealTimeInfo(Guid id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.CRMConn);
            RealTimeInfo result = new RealTimeInfo();
            try
            {

                SqlCommand cmd = new SqlCommand("select hr_coupon_status_check, hr_couponstatus_rapidcheck, hr_crm_agency_status, hr_ticket_numbers, hr_taxes_rapid from hr_refund_application where hr_refund_applicationid='" + id.ToString() + "'", conn);
                string tasas="";
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader sr = cmd.ExecuteReader();
                if (sr.Read())
                {
                    result.RefundId = id;
                    if (!sr.IsDBNull(0)) result.CouponStatusCheck1A = sr.GetString(0);
                    if (!sr.IsDBNull(1)) result.CouponStatusCheckRapid=sr.GetString(1);
                    if (!sr.IsDBNull(2)) result.CRMAgencyStatus = sr.GetString(2);
                    if (!sr.IsDBNull(3)) result.TicketInCRM = sr.GetString(3);
                    if (!sr.IsDBNull(4)) tasas = sr.GetString(4);
                    result.CouponStatusCheck1Acolor = (result.CouponStatusCheck1A != "ok");
                    result.CouponStatusCheckRapidcolor = (result.CouponStatusCheckRapid != "ok");
                    result.CRMAgencyStatuscolor = (result.CRMAgencyStatus != "ok");
                }
                sr.Close();

                if (tasas != "")
                {
                    string taxsales = "";
                    string taxunref = "";
                    string[] tasasa = tasas.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ta in tasasa)
                    {
                        string t = ta.Split(' ')[0];
                            if (t.Length == 2)
                            {
                                cmd.CommandText = String.Format("select hr_non_refundable, hr_sales_tax from hr_taxes where hr_tax = '{0}'", t);
                                sr = cmd.ExecuteReader();
                                if (sr.Read())
                                {
                                    if (!sr.IsDBNull(0) && sr[0].ToString() == "True") taxunref += " " + t;
                                    if (!sr.IsDBNull(1) && sr[1].ToString() == "True") taxsales += " " + t;
                                }
                            sr.Close();
                        }
                    }
                    result.Tax_NonRefund = taxunref.Trim();
                    result.Tax_Sales = taxsales.Trim();
                }

                if (conn.State != ConnectionState.Closed) conn.Close();
            }
            catch (SqlException sqlE)
            {
                throw new Exception("SQL Exception - " + sqlE.Message);
            }
            catch (Exception e)
            {

                throw new Exception("General Exception - " + e.Message);
            }
            return result;
        }
    }
}