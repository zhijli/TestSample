using System;

namespace TestSample
{
    using global::Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Security.Permissions;
    using System.Security.Principal;
    using System.Threading;

    [TestClass]
    public class IdentitySample
    {
        [TestMethod]
        public void Test1()
        {
            // Retrieve the Windows account token for the current user.
            IntPtr logonToken = LogonUser();

            // Constructor implementations.
            IntPtrConstructor(logonToken);
            IntPtrStringConstructor(logonToken);
            IntPtrStringTypeConstructor(logonToken);
            IntPrtStringTypeBoolConstructor(logonToken);

            // Property implementations.
            UseProperties(logonToken);

            // Method implementations.
            GetAnonymousUser();
            ImpersonateIdentity(logonToken);

            Console.WriteLine("This sample completed successfully; " +
                "press Enter to exit.");
        }

        [TestMethod]
        public void Test2()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(
                PrincipalPolicy.NoPrincipal);
            var identity = Thread.CurrentPrincipal.Identity as WindowsIdentity;

            Console.WriteLine("Current user: {0}", Thread.CurrentPrincipal.Identity.Name);

            var newIdentity = new GenericIdentity("zhijie Li");
            var roles = new string[] { "Admin", "Users" };
            var newPrincipal = new GenericPrincipal(newIdentity, roles);
            //var newIdentity = new WindowsIdentity("zhijie li");
            //var newPrincipal = new WindowsPrincipal(newIdentity);
            Thread.CurrentPrincipal = newPrincipal;

            Console.WriteLine("Current user: {0}", Thread.CurrentPrincipal.Identity.Name);


            Console.WriteLine("This sample completed successfully; " +
               "press Enter to exit.");
        }

        [TestMethod]
        public void Test3()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(
                PrincipalPolicy.UnauthenticatedPrincipal);

            try
            {
                // Create a generic identity.
                GenericIdentity MyIdentity = new GenericIdentity("MyUser");

                // Create a generic principal.
                String[] MyString = { "Administrator", "User" };

                GenericPrincipal MyPrincipal =
                    new GenericPrincipal(MyIdentity, MyString);

                Thread.CurrentPrincipal = MyPrincipal;

                // Create a PrincipalPermission object.
                PrincipalPermission MyPermission =
                    new PrincipalPermission("MyUser", "Administrator");

                // Demand this permission.
                MyPermission.Demand();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void Test4()
        {
            // Create a new thread with a generic principal.
            Thread t = new Thread(new ThreadStart(PrintPrincipalInformation));
            t.Start();
            t.Join();

            // Set the principal policy to WindowsPrincipal.
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            // The new thread will have a Windows principal representing the
            // current user.
            t = new Thread(new ThreadStart(PrintPrincipalInformation));
            t.Start();
            t.Join();

            // Create a principal to use for new threads.
            IIdentity identity = new GenericIdentity("NewUser");
            IPrincipal principal = new GenericPrincipal(identity, null);
            currentDomain.SetThreadPrincipal(principal);

            // Create a new thread with the principal created above.
            t = new Thread(new ThreadStart(PrintPrincipalInformation));
            t.Start();
            t.Join();
        }

        [TestMethod]
        public void Test5()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var identity = Thread.CurrentPrincipal.Identity as WindowsIdentity;

            var token = identity.Token;
            foreach (var clain in identity.Claims)
            {
               Console.WriteLine(clain.ReportAllProperties());
            }
        }

        private static void PrintPrincipalInformation()
        {
            IPrincipal curPrincipal = Thread.CurrentPrincipal;
            if (curPrincipal != null)
            {
                Console.WriteLine("Type: " + curPrincipal.GetType().Name);
                Console.WriteLine("Name: " + curPrincipal.Identity.Name);
                Console.WriteLine("Authenticated: " +
                    curPrincipal.Identity.IsAuthenticated);
                Console.WriteLine();
            }
        }

        // Create a WindowsIdentity object for the user represented by the
        // specified Windows account token.
        private static void IntPtrConstructor(IntPtr logonToken)
        {
            // Construct a WindowsIdentity object using the input account token.
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);

            Console.WriteLine("Created a Windows identity object named " +
                windowsIdentity.Name + ".");
        }


        // Create a WindowsIdentity object for the user represented by the
        // specified account token and authentication type.
        private static void IntPtrStringConstructor(IntPtr logonToken)
        {
            // Construct a WindowsIdentity object using the input account token 
            // and the specified authentication type.
            string authenticationType = "WindowsAuthentication";
            WindowsIdentity windowsIdentity =
                            new WindowsIdentity(logonToken, authenticationType);

            Console.WriteLine("Created a Windows identity object named " +
                windowsIdentity.Name + ".");
        }

        // Create a WindowsIdentity object for the user represented by the
        // specified account token, authentication type, and Windows account
        // type.
        private static void IntPtrStringTypeConstructor(IntPtr logonToken)
        {
            // Construct a WindowsIdentity object using the input account token,
            // and the specified authentication type, and Windows account type.
            string authenticationType = "WindowsAuthentication";
            WindowsAccountType guestAccount = WindowsAccountType.Guest;
            WindowsIdentity windowsIdentity =
                new WindowsIdentity(logonToken, authenticationType, guestAccount);

            Console.WriteLine("Created a Windows identity object named " +
                windowsIdentity.Name + ".");
        }

        // Create a WindowsIdentity object for the user represented by the
        // specified account token, authentication type, Windows account type, and
        // Boolean authentication flag.
        private static void IntPrtStringTypeBoolConstructor(IntPtr logonToken)
        {
            // Construct a WindowsIdentity object using the input account token,
            // and the specified authentication type, Windows account type, and
            // authentication flag.
            string authenticationType = "WindowsAuthentication";
            WindowsAccountType guestAccount = WindowsAccountType.Guest;
            bool isAuthenticated = true;
            WindowsIdentity windowsIdentity = new WindowsIdentity(
                logonToken, authenticationType, guestAccount, isAuthenticated);

            Console.WriteLine("Created a Windows identity object named " +
                windowsIdentity.Name + ".");
        }
        // Access the properties of a WindowsIdentity object.
        private static void UseProperties(IntPtr logonToken)
        {
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);
            string propertyDescription = "The Windows identity named ";

            // Retrieve the Windows logon name from the Windows identity object.
            propertyDescription += windowsIdentity.Name;

            // Verify that the user account is not considered to be an Anonymous
            // account by the system.
            if (!windowsIdentity.IsAnonymous)
            {
                propertyDescription += " is not an Anonymous account";
            }

            // Verify that the user account has been authenticated by Windows.
            if (windowsIdentity.IsAuthenticated)
            {
                propertyDescription += ", is authenticated";
            }

            // Verify that the user account is considered to be a System account
            // by the system.
            if (windowsIdentity.IsSystem)
            {
                propertyDescription += ", is a System account";
            }
            // Verify that the user account is considered to be a Guest account
            // by the system.
            if (windowsIdentity.IsGuest)
            {
                propertyDescription += ", is a Guest account";
            }

            // Retrieve the authentication type for the 
            String authenticationType = windowsIdentity.AuthenticationType;

            // Append the authenication type to the output message.
            if (authenticationType != null)
            {
                propertyDescription += (" and uses " + authenticationType);
                propertyDescription += (" authentication type.");
            }

            Console.WriteLine(propertyDescription);

            // Display the SID for the owner.
            Console.Write("The SID for the owner is : ");
            SecurityIdentifier si = windowsIdentity.Owner;
            Console.WriteLine(si.ToString());
            // Display the SIDs for the groups the current user belongs to.
            Console.WriteLine("Display the SIDs for the groups the current user belongs to.");
            IdentityReferenceCollection irc = windowsIdentity.Groups;
            foreach (IdentityReference ir in irc)
            {
                string account = new System.Security.Principal.SecurityIdentifier(ir.Value).Translate(typeof(System.Security.Principal.NTAccount)).ToString();
                Console.WriteLine("Sid: {0}, Name:{1}", ir.Value,account);
            }
            TokenImpersonationLevel token = windowsIdentity.ImpersonationLevel;
            Console.WriteLine("The impersonation level for the current user is : " + token.ToString());
        }

        // Retrieve the account token from the current WindowsIdentity object
        // instead of calling the unmanaged LogonUser method in the advapi32.dll.
        private static IntPtr LogonUser()
        {
            IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
            Console.WriteLine("Token number is: " + accountToken.ToString());

            return accountToken;
        }

        // Get the WindowsIdentity object for an Anonymous user.
        private static void GetAnonymousUser()
        {
            // Retrieve a WindowsIdentity object that represents an anonymous
            // Windows user.
            WindowsIdentity windowsIdentity = WindowsIdentity.GetAnonymous();
        }

        // Impersonate a Windows identity.
        private static void ImpersonateIdentity(IntPtr logonToken)
        {
            // Retrieve the Windows identity using the specified token.
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);

            // Create a WindowsImpersonationContext object by impersonating the
            // Windows identity.
            WindowsImpersonationContext impersonationContext =
                windowsIdentity.Impersonate();

            Console.WriteLine("Name of the identity after impersonation: "
                + WindowsIdentity.GetCurrent().Name + ".");
            Console.WriteLine(windowsIdentity.ImpersonationLevel);
            // Stop impersonating the user.
            impersonationContext.Undo();

            // Check the identity name.
            Console.Write("Name of the identity after performing an Undo on the");
            Console.WriteLine(" impersonation: " +
                WindowsIdentity.GetCurrent().Name);
        }
    }
}
