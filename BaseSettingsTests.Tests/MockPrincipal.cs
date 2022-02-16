using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace BaseSettingsTests.Tests
{

    public enum MockPrincipalBehavior
    {
        AlwaysReturnTrue,
        WhiteList,
        BlackList
    }

    public class MockPrincipal: IPrincipal, IIdentity
    {
        private HashSet<String> Roles { get; set; }
        public MockPrincipalBehavior Behavior { get; set; }

        public MockPrincipal(String name = "TestUser", MockPrincipalBehavior behavior = MockPrincipalBehavior.AlwaysReturnTrue)
        {
            Roles = new HashSet<String>();
            Name = name;
            IsAuthenticated = true;
            AuthenticationType = "FakeAuthentication";
        }

        public void AddRoles(params String[] roles)
        {
            Behavior = MockPrincipalBehavior.WhiteList;

            if (roles == null || roles.Length == 0) return;

            var rolesToAdd = roles.Where(r => !Roles.Contains(r));

            foreach (var role in rolesToAdd)
                Roles.Add(role);
        }

        public void IgnoreRoles(params String[] roles)
        {
            Behavior = MockPrincipalBehavior.BlackList;

            AddRoles(roles);
        }

        public void RemoveRoles(params String[] roles)
        {
            if (roles == null || roles.Length == 0) return;

            var rolesToAdd = roles.Where(r => Roles.Contains(r));

            foreach (var role in rolesToAdd)
                Roles.Remove(role);
        }

        public void RemoveAllRoles()
        {
            Roles.Clear();
        }

        #region IPrincipal Members

        public IIdentity Identity { get { return this; } }

        public bool IsInRole(string role)
        {
            if (Behavior == MockPrincipalBehavior.AlwaysReturnTrue)
                return true;

            var isInlist = Roles.Contains(role);

            if (Behavior == MockPrincipalBehavior.BlackList)
                return !isInlist;

            return isInlist;
        }

        #endregion

        #region IIdentity Members

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        #endregion
    }
}
