using System;
using System.Globalization;
using System.Net;
using System.Security;
using System.Net.Mail;

namespace Sc2Hack.Classes.BackEnds
{
    public class Messages
    {

        /* Send a mail to me.. */
        public static void SendEmail(String smtp, String authSenderUser, SecureString authSenderPassword,
                                     MailAddress mailSendTo, String msgTitle, String msgBody,
                                     String msgAdditional)
        {
            msgTitle += " - [" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "]";
            msgBody = "Description:\n" +
                      "############\n" +
                      msgBody;

            msgBody += "\n\n\nAdditional Information:\n" +
                        "############\n" + 
                        msgAdditional;

            

            var mailmsg = new MailMessage(mailSendTo, mailSendTo);
            mailmsg.Body = msgBody + msgAdditional;
            mailmsg.Subject = msgTitle;
            mailmsg.Attachments.Add(new Attachment(Constants.StrDummyPref));

            var smtpServ = new SmtpClient(smtp);
            smtpServ.Credentials = new NetworkCredential(authSenderUser, authSenderPassword);

            var iCounter = 0;

            SendAgain:
            try
            {
                 smtpServ.Send(mailmsg);
            }

            catch
            {
                if (iCounter < 7)
                {
                    iCounter++;
                    goto SendAgain;
                }
            }
        }
    }
}
