using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Reflection;

namespace SISTEMATICKET
{
    public partial class frmCrearTICKET : System.Web.UI.Page
    {
        

        [WebMethod]
        public static Respuesta<int> Guardar(string xml)
        {
            xml = xml.Replace("!idusuario¡", Configuracion.oUsuario.IdUsuario.ToString());
            int Respuesta = 0;
            Respuesta = CD_TICKET.Instancia.RegistrarTICKET(xml);
            if (Respuesta != 0) 
                return new Respuesta<int>() { estado = true, valor = Respuesta.ToString() };
            else
                return new Respuesta<int>() { estado = false }; 
            
        }

        [WebMethod]
        public static Respuesta<List<REQUERIMIENTOAREA>> ObtenerREQUERIMIENTOxAREA(int IdAREA)
        {
            List<REQUERIMIENTOAREA> oListaREQUERIMIENTOAREA = CD_REQUERIMIENTOAREA.Instancia.ObtenerREQUERIMIENTOAREA();
            oListaREQUERIMIENTOAREA = oListaREQUERIMIENTOAREA.Where(x => x.oAREA.IdAREA == IdAREA && x.Stock > 0).ToList();


            if (oListaREQUERIMIENTOAREA != null)
            {
                return new Respuesta<List<REQUERIMIENTOAREA>>() { estado = true, objeto = oListaREQUERIMIENTOAREA };
            }
            else
            {
                return new Respuesta<List<REQUERIMIENTOAREA>>() { estado = false, objeto = null };
            }
        }

        [WebMethod]
        public static Respuesta<bool> ControlarStock(int idREQUERIMIENTO, int IdAREA, int cantidad, bool restar)
        {
            bool Respuesta = false;
            Respuesta = CD_REQUERIMIENTOAREA.Instancia.ControlarStock(idREQUERIMIENTO, IdAREA, cantidad, restar);
            return new Respuesta<bool>() { estado = Respuesta };
        }

        private void SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            var host = ConfigurationManager.AppSettings["Host"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
            var from = ConfigurationManager.AppSettings["SMTPuser"];
            var username = ConfigurationManager.AppSettings["SMTPuser"];
            var password = ConfigurationManager.AppSettings["SMTPpassword"];
            var ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);

            using (var smtpClient = new SmtpClient(host, port))
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = ssl;

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(from);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;    
                    mailMessage.IsBodyHtml = isBodyHtml;
                    mailMessage.To.Add(new MailAddress(to));
                    smtpClient.Send(mailMessage);
                }
            }
        }

        protected void btnTerminarGuardarTICKET_Click(object sender, EventArgs e)
        {
            int idTicket = Convert.ToInt32(Ticket.Value); 
            TICKET oTICKET = new TICKET();
           oTICKET = CD_TICKET.Instancia.ObtenerDetalleTICKET(idTicket);


            string date = DateTime.Now.ToString("ddd dd/MM/yy hh:mm:ss tt");
            string body = body = "<table border=\"1\" style=\"background-color:#000000\"><tr bgcolor=\"#CA515C\"><th><font color=\"#FFFFFF\">Matic N°</font></th><th><font color=\"#FFFFFF\">Tiempo de Ejecución</font></th><th colspan=\"2\"><font color=\"#FFFFFF\">Solicitado Por</font></th></tr><tr bgcolor=\"#D9D9D9\"><td>" + oTICKET.Codigo+"</td><td>"+date+"</td><td>"+oTICKET.oUsuario.Nombres+"</td><td>"+oTICKET.oUsuario.Apellidos+"</td></tr></table>";
           
            body = body + "<br/><table id=\"tbTICKET\" border=\"1\" style=\"background-color:#000000\" style=\"width: 500px;\"><thead><tr bgcolor=\"#CA515C\"><th style=\"width: 45%;\"><font color=\"#FFFFFF\">Solicitud</font></th></tr></thead><tbody>";
            foreach (var item in oTICKET.oListaDetalleTICKET)
            {
                body = body + "<tr bgcolor=\"#D9D9D9\"><td><center>" + item.NombreREQUERIMIENTO + "</center></td></tr>";
            }
            body = body + "</tbody></table>";

            body = body + "<br/><table border=\"1\" style=\"background-color:#FFFFFF\"><tr bgcolor=\"#FFFFFF\"><th><font color=\"#000000\">Enviado desde</font></th><td>" + "⇒" + oTICKET.oAREA.Nombre+ " </td></tr><tr bgcolor=\"#FFFFFF\"><th><font color=\"#000000\">Dirección</font></th><td>" + oTICKET.oAREA.Direccion+ "</td></tr><tr bgcolor=\"#FFFFFF\"><th rowspan=\"2\"><font color=\"#000000\">Más</font></th><td>" + oTICKET.oDATOS.Nombre+ "</td></tr><tr bgcolor=\"#FFFFFF\"><td>" + oTICKET.oDATOS.NumeroDocumento+"</td></tr></table>";

            DropDownList dllEmail = Page.FindControl("ddlEmail") as DropDownList;

            SendAsync(ddlEmail.SelectedItem.Value.ToString(), "Ticket Creado N° [" + oTICKET.Codigo +"]", body);
            Response.Write("alert('Email Sent..');");

            


        }
    }

}