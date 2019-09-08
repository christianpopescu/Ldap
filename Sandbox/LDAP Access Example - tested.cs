using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;



namespace ConsoleApplication7
{
    class Program
    {

        static void Main0(string[] args)
        {
            string groupName = "L_FMX_APP__DEV";
            string domainName = "BIC";
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domainName);
            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groupName);

            if (grp != null)
            {
                foreach (Principal p in grp.GetMembers(true))
                {
                    Console.WriteLine(p.Name + "    -   " + p.DisplayName + "      -      " + p.UserPrincipalName); //You can add more attributes, samaccountname, UPN, DN, object type, etc... 
                }


                grp.Dispose();
                ctx.Dispose();

            }
            else
            {
                Console.WriteLine("\nWe did not find that group in that domain, perhaps the group resides in a different domain?");
            }
        }
        static void Main(string[] args)
        {
            String ldapServerAddress = "srv01.smt.bic.net";
            String strUrl = "LDAP://" + ldapServerAddress + "/DC=" + "BIC" + ",DC=net";
            DirectoryEntry root = new DirectoryEntry(strUrl);

            DirectorySearcher searcher = new DirectorySearcher(root);


            String userName = "christian";

            // Research by name
            searcher.Filter = "(cn=" + userName + ")";

            
            searcher.PropertiesToLoad.Add("memberOf");

            SearchResultCollection results;

            results = searcher.FindAll();

            int n = results[0].Properties["memberOf"].Count;

            for (int i = 0; i < n; i++)
            {
//                if (((String)results[0].Properties["memberOf"][i]).Contains("Doom"))
                Console.WriteLine(results[0].Properties["memberOf"][i]);
            }


            Console.WriteLine("Bye!");

        }
    }
}
