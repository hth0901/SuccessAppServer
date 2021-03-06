﻿using Microsoft.AspNet.Identity;
using SuccessAppService.Framework.Access;
using SuccessAppService.Framework.DAL;
using SuccessAppService.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SuccessAppService.Framework.IdentityStore
{
    public class UserStore: IUserStore<ApplicationUser>, IUserRoleStore<ApplicationUser>,  IUserEmailStore<ApplicationUser>
    {
        #region IUserStore
        public Task CreateAsync(ApplicationUser user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    user.Id = Guid.NewGuid().ToString();
                    //UserController.NewUser(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    //UserController.DeleteUser(user);
                });
            }
            throw new ArgumentNullException("user");
        }

        public void Dispose()
        {

        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return Task.Factory.StartNew(() =>
                {
                    return UserController.GetUser(userId);
                });
            }
            throw new ArgumentNullException("userId");
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return Task.Factory.StartNew(() =>
                {
                    //return UserController.GetUserByUsername(userName);
                    return aUserAccess.getUserByUserName(userName);
                });
            }
            throw new ArgumentNullException("userName");
        }

        public ApplicationUser FindByName(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return UserController.GetUserByUsername(userName);
            }
            throw new ArgumentNullException("userName");
        }

        public ApplicationUser FindByNameAndPassword(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return UserController.GetUserByUsername(userName);
            }
            throw new ArgumentNullException("userName");
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    return UserController.UpdateUser(user);
                });
            }
            throw new ArgumentNullException("userName");
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return Task.Factory.StartNew(() =>
                {
                    //return UserController.GetUserByUsername(email);
                    return aUserAccess.getUserByEmail(email);
                });
            }
            throw new ArgumentNullException("email");
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    UserRoleController.NewUserRole(user.Id, roleName);
                });
            }
            else
            {
                throw new ArgumentNullException("user");
            }
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    UserRoleController.DeleteUserRole(user.Id, roleName);
                });
            }
            else
            {
                throw new ArgumentNullException("user");
            }
        }

        //public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        //{
        //    if (user != null)
        //    {
        //        return Task.Factory.StartNew(() =>
        //        {
        //            IList<string> roles = UserRoleController.GetUserRoles(user.Id);
        //            return roles;
        //        });
        //    }
        //    else
        //    {
        //        throw new ArgumentNullException("user");
        //    }
        //}

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    //IList<string> roles = UserRoleController.GetUserRoles(user.Id);
                    //foreach (string role in roles)
                    //{
                    //    if (role.ToUpper() == roleName.ToUpper())
                    //    {
                    //        return true;
                    //    }
                    //}

                    return false;
                });
            }
            else
            {
                throw new ArgumentNullException("user");
            }
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        //public Task FindAsync(string userName, string password)
        //{
        //    if (!string.IsNullOrEmpty(userName))
        //    {
        //        return Task.Factory.StartNew(() =>
        //        {
        //            //return UserController.GetUserByUsername(userName);
        //            return aUserAccess.getUserByUserName(userName);
        //        });
        //    }
        //    throw new ArgumentNullException("userName");
        //}
        #endregion
        //Task CreateAsync(TUser user);
        //Task DeleteAsync(TUser user);
        //Task<TUser> FindByIdAsync(TKey userId);
        //Task<TUser> FindByNameAsync(string userName);
        //Task UpdateAsync(TUser user);
    }
}