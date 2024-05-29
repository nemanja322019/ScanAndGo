using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Helpers.EmailBody
{
    public static class ResetPasswordEmailBody
    {
        public static string ResetPasswordEmailStringBody(string email, string password)
        {
            return $@"<html><body>
    <table class=""email-wrapper"" width=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
      <tr>
        <td align=""center"">
          <table class=""email-content"" width=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
            <tr>
              <td class=""email-masthead"">
              </td>
            </tr>
            <!-- Email Body -->
            <tr>
              <td class=""email-body"" width=""570"" cellpadding=""0"" cellspacing=""0"">
                <table class=""email-body_inner"" align=""center"" width=""570"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                  <!-- Body content -->
                  <tr>
                    <td class=""content-cell"">
                      <div class=""f-fallback"">
                        <h1>Hi {email},</h1>
                        <p>You recently requested to reset your password for your ScanAndGo account. Use this temporary password to login to your account:</p>
                        <!-- Action -->
                        <table class=""body-action"" align=""center"" width=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                          <tr>
                            <td align=""center"">
                              <!-- Border based button
                                https://litmus.com/blog/a-guide-to-bulletproof-buttons-in-email-design -->
                              <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
                                <tr>
                                  <td align=""center"">
                                    {password}
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <p><strong>This temporary password is only valid for the next 24 hours<strong>, so don't forget to change it.</p>
                        <p>Thanks,
                          <br>The ScanAndGo team</p>
                      </div>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table class=""email-footer"" align=""center"" width=""570"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                  <tr>
                    <td class=""content-cell"" align=""center"">
                      <p class=""f-fallback sub align-center"">
                        [ScanAndGo, LLC]
                        <br>1234 Street Rd.
                        <br>Suite 1234
                      </p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>";
        }
    }
}
