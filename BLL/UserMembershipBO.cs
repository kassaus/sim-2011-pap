using System;
using System.Collections.Generic;
using BO;
using DAL;

namespace BLL
{


  public class UserMembershipBO
  {
    private UserMembershipDO membershipDataManager { get; set; }

    public UserMembershipBO()
    {
      membershipDataManager = new UserMembershipDO();
    }

    public bool modificaEstadoLock(Guid id_user, bool estado)
    {
      aspnet_Membership user = new aspnet_Membership();
      user.UserId = id_user;
      user.IsLockedOut = estado;

      return membershipDataManager.updateLockState(user);
    }


  }
}
