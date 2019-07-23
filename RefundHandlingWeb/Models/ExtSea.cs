using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefundHandlingWeb.Models
{
    public class ExtSea
    {
        public List<respuesta> respuestas = new List<respuesta>();
        public List<respuesta> fillRespuestas(string TicketNumbers, Guid id)
        {

            string[] Tickets = TicketNumbers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); 

            //string Ticket=tickets;
            ServiceReference1.ExtendedSearchServiceClient client = new ServiceReference1.ExtendedSearchServiceClient();

            foreach (string Ticket in Tickets)
            {
                string TicketNr = Ticket.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                string[] results = { "" };
                if (TicketNr.Length == 10)
                {
                    try
                    {
                        results = client.SearchInRedis(TicketNr);
                    }
                    catch (Exception exc)
                    {
                        results[0] = ";;;;Error while searching Ticket in CRM;;;;;;;;";
                    }
                }
                foreach (string result in results)
                    AddRows(result, id);
            }
            respuestas = respuestas.OrderBy(x => x.CreatedOn).ToList();
            return respuestas ;
        }
        private void AddRows(string result, Guid refundid)
        {
            respuesta resp = new respuesta();

            var resultSplited = result.Split(';');
             
            Guid id= Guid.Parse(resultSplited[0]);

            if (refundid==id||respuestas.Where(ent => ent.id == id).Count() != 0) return;

            string entity = "";

            switch (resultSplited[1])
            {
                case "A":
                    resp.Type = "ADMs";
                    entity = "hr_agency_memo";
                    resp.img = "/Content/hr_ico_Agency_memo16.ico";
                    //resp.img = "imgs/ico_16_customEntity.gif";
                    break;
                case "C":
                    resp.Type = "Cases";
                    entity = "incident";
                    resp.img = "/Content/ico_16_112.gif";
                    break;
                case "E":
                    resp.Type = "Emails";
                    entity = "email";
                    resp.img = "/Content/ico_16_bulkemail.gif";
                    break;
                case "N":
                    resp.Type = "Notes";
                    entity = "annotation";
                    resp.img = "/Content/ico_18_5.gif";
                    break;
                case "R":
                    resp.Type = "Refunds";
                    entity = "hr_refund_application";
                    //resp.img = "imgs/ico_16_customEntity.gif";
                    resp.img = "/Content/hr_ico_refund_application16.png";
                    break;

            }
            resp.CreatedOn = DateTime.Parse(resultSplited[2]);
            resp.CreatedBy = resultSplited[4];
            resp.ModifiedBy = resultSplited[3];
            resp.Title = resultSplited[5].Replace("&#59", ";");
            resp.Ticketnumbers = resultSplited[6].Replace("&#59", ";");
            resp.Reason = resultSplited[7].Replace("&#59", ";");
            resp.AdditionalInformation = resultSplited[8].Replace("&#59", ";");
            resp.DateV = resultSplited[9] == "true";
            resp.Agency = resultSplited[10];
            resp.Agent = resultSplited[11];
            resp.Country = resultSplited[12];
            resp.Link = "http://mscrm/hrcrm/main.aspx?etn=" + entity + "&pagetype=entityrecord&id=" + id;
            resp.id = id;
            respuestas.Add(resp);

        }
        public class respuesta
        {
            public string Type { get; set; }
            public DateTime CreatedOn { get; set; }
            //public DateTime ModifiedOn { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedBy { get; set; }
            public string Title { get; set; }
            public string Ticketnumbers { get; set; }
            public string Reason { get; set; }
            public string AdditionalInformation { get; set; }
            public bool DateV { get; set; }
            public string Agency { get; set; }
            public string Agent { get; set; }
            public string Country { get; set; }
            public string Link { get; set; }
            public string img { get; set; }
            public Guid id { get; set; }
        }
    }
}