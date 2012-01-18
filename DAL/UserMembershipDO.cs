using System;
using System.Collections.Generic;
using System.Data.Objects;
using BO;
using System.Linq;


namespace DAL
{

    public class UserMembershipDO
    {

        public aspnet_Membership obterUser(Guid userId)
        {
            aspnet_Membership aux = null;
            try
            {
                aux = (from user in DB.tabelas.aspnet_Membership where user.UserId == userId select user).FirstOrDefault<aspnet_Membership>();
            }
            catch { }

            return aux;
        }


        public bool updateLockState(aspnet_Membership user)
        {
            bool sucesso = false;
            aspnet_Membership aux = null;
            try
            {
                if (user.EntityState == System.Data.EntityState.Detached)
                {
                    aux = obterUser(user.UserId);
                    if (aux == null)
                    {
                        return false;
                    }
                    aux.IsLockedOut = user.IsLockedOut;
                }
                sucesso = (DB.tabelas.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }


    }
}
