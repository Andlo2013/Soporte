using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsMVC.Areas.supportSI.Models
{
    public class _infoUser
    {
        private string m_email = "";
        private int m_empresaID = 0;
        private string m_userName = "";
        private string m_userID = "";
        public _infoUser(string email,int empresaID,string userName,string userID)
        {
            m_email = email;
            m_empresaID = empresaID;
            m_userName = userName;
            m_userID=userID;
        }

        public int pro_empresaID { get { return m_empresaID; } }

        public string pro_email { get { return m_email; } }

        public string pro_userID { get { return m_userID; } }

        public string pro_userName { get { return m_userName; } }
    }
}