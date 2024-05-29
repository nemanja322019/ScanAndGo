using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.WebRequestMethods;

namespace ServiceLibrary.Helpers.EmailBody
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            string emailEncoded = HttpUtility.UrlEncode(email);
            string emailTokenEncoded = HttpUtility.UrlEncode(emailToken);

            string deepLink = "scanandgoflutter://reset-password?email=" + emailEncoded + "&emailToken=" + emailTokenEncoded;
            string uri = "https://scanandgoapi.azurewebsites.net/api/BuyerAuth/redirect-to-deep-link?url=" + HttpUtility.UrlEncode(deepLink);

            return $@"<html><body>
    <span class=""preheader"">Use this link to reset your password. The link is only valid for 24 hours.</span>
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
                        <p>You recently requested to reset your password for your ScanAndGo account. Use the button below to reset it. <strong>This password reset is only valid for the next 24 hours.</strong></p>
                        <!-- Action -->
                        <table class=""body-action"" align=""center"" width=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                          <tr>
                            <td align=""center"">
                              <!-- Border based button
                                https://litmus.com/blog/a-guide-to-bulletproof-buttons-in-email-design -->
                              <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" role=""presentation"">
                                <tr>
                                  <td align=""center"">
                                    <a href=""{uri}"" class=""f-fallback email-masthead_name"">
                                    Reset your password</a>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <p>If you did not request a password reset, please ignore this email or <a href=""{{{{}}}}"">contact support</a> if you have questions.</p>
                        <p>Thanks,
                          <br>The ScanAndGo team</p>
                        <!-- Sub copy -->
                        <table class=""body-sub"" role=""presentation"">
                          <tr>
                            <td>
                              <p class=""f-fallback sub"">If you’re having trouble with the button above, copy and paste the URL below into your web browser.</p>
                              <p class=""f-fallback sub"">{{{{{uri}}}}}</p>
                            </td>
                          </tr>
                        </table>
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
