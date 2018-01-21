using System;
namespace LoginSystem
{
    public static class ConnectedUser
    {
        public static string AWSAccessToken
        {
            get { return AWSAccessToken; }
            set { AWSAccessToken = value; }
        }
       
        public static string SoftAccessToken 
        {
            get { return SoftAccessToken; }
            set { SoftAccessToken = value; }
        }

    }
}
