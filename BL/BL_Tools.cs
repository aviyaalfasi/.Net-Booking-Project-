using System;
using System.Net.Mail;
using System.Threading;
using BE;
namespace BL
{
     class BL_Tools
    {
        


        /// <summary>
        /// returns the number of digits in int
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        internal static int numDigits(long num)
        {
            int sum = 0;

            while (num != 0)
            {
                sum ++;
                num = num / 10;
            }

            return sum;
        }
       
        /// <summary>
        /// general function that send a mail
        /// </summary>
        /// <param name="dstMail">destination mail address</param>
        /// <param name="subject"> subject of meesege</param>
        /// <param name="body">body of messege</param>
        /// <param name="isBodyHmtl">format of messege</param>
        public static void GeneralSendMail(string dstMail, string subject, string body, bool isBodyHmtl = false)
        {
            SiteOwner owner = SiteOwnerDetails.getSiteOwner();
            new Thread(() =>
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(dstMail);
                mail.From = new MailAddress(owner.MailAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHmtl;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential(owner.MailAddress, owner.MailPassword);
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ).Start();
        }
    }
}
