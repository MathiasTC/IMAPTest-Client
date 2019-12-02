using System;
using MailKit;
using MailKit.Net.Imap;

namespace IMAPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ImapClient())
            {
                // Accepts all certificates (UNSAFE!!)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("Insert mailserver here", 993, true);                
                client.Authenticate(@"Insert username here", "Insert PW here");
                                
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);             

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    Console.WriteLine($"Mail nr. {i}");
                    Console.WriteLine($"Subject: {message.Subject}");
                    Console.WriteLine($"Body: {message.TextBody}");
                    Console.WriteLine("---------------------------------");
                }
                Console.WriteLine($"Total tickets: {inbox.Count}");
                Console.ReadLine();
                client.Disconnect(true);
            }
        }
    }
}
