using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon.CognitoIdentityProvider.Model;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Amazon.Runtime;

namespace LoginSystem
{
    public class OnSignUpEventArgs : EventArgs
    {

        private string mFirstName;
        private string mEmail;
        private string mPassword;

        public OnSignUpEventArgs(string firstName, string email, string password)
        {

            FirstName = firstName;
            Email = email;
            Password = password;

            OnSignUpEventArgsf(FirstName, Email, Password);

        }

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public async void OnSignUpEventArgsf(string firstName, string email, string password)
        {

            AmazonUtils.CognitoUserId = FirstName;
            CognitoUser user = AmazonUtils.CognitoUser;

            InitiateSrpAuthRequest initiateSrpAuthRequest = new InitiateSrpAuthRequest();
            initiateSrpAuthRequest.Password = Password;

            var response = await user.StartWithSrpAuthAsync(initiateSrpAuthRequest).ConfigureAwait(false);
            while (response.AuthenticationResult == null)
            {
                if (response.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                {
                    string newPassword = "Omniwallet1";

                    response =
                await user.RespondToNewPasswordRequiredAsync(new RespondToNewPasswordRequiredRequest()
                {
                    SessionID = response.SessionID,
                    NewPassword = newPassword
                    }).ConfigureAwait(false);
                }

                Console.WriteLine("ICITE" + response.AuthenticationResult.AccessToken);
                GetUserRequest getUserRequest = new GetUserRequest();
                getUserRequest.AccessToken = response.AuthenticationResult.AccessToken;

                var getTheUser = AmazonUtils.IdentityClientProvider.GetUserAsync(getUserRequest);

                var userAttributes = getTheUser.Result.UserAttributes;

                user.Attributes = new Dictionary<string, string>();

                if (userAttributes != null)
                {
                    for (int i = 0; i < userAttributes.Count; ++i)
                    {
                        // Print out the attributes that are stored in Cognito User Pool for the user
                        user.Attributes.Add(userAttributes[i].Name, userAttributes[i].Value);
                        System.Diagnostics.Debug.WriteLine(string.Format("Name: {0} Value: {1}", userAttributes[i].Name, userAttributes[i].Value));
                    }
                }
            }
            if (response.AuthenticationResult != null)
            {
                Console.WriteLine("Liiiit");
            }
        }
    }
          

    class dialog_SignUp : DialogFragment
    {
        private EditText mTxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignUp;

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mTxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            mBtnSignUp.Click += mBtnSignUp_Click;

            return view;
        }

        void mBtnSignUp_Click(object sender, EventArgs e)
        {
           //User has clicked the sign up button
            mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    }


    class AmazonUtils
    {
        private static CognitoUser cognitoUser;
        public static string mClientId = "4bcsplem7avm4qio3sqg233oi8";
        public static string mAuthPoolId = "us-east-1_fnfv23BsX";
        public static Amazon.RegionEndpoint mRegion = Amazon.RegionEndpoint.USEast1;
        public static string CognitoUserId = string.Empty;
        public static CognitoUser CognitoUser
        {
            get
            {
                if (cognitoUser == null)
                {
                    cognitoUser = GetCurrentUser(CognitoUserId);
                }
                return cognitoUser;
            }
        }

        // Get current user to verify password
        private static CognitoUser GetCurrentUser(string userId)
        {
            CognitoUser tempUser = new CognitoUser(userId, mClientId, UserPool, IdentityClientProvider);
            return tempUser;
        }

        // Reference the user pool where the user data is stored
        private static CognitoUserPool userPool;
        public static CognitoUserPool UserPool
        {
            get
            {
                if (userPool == null)
                {
                    // AuthPool Id is the User Pool Id which can be found in the aws console under Cognito>User Pool>>GeneralSettings>Pool Id
                    // Client Id is App Client ID from when setting up an app in the Cognito>User Pool>App clients>App Client Id
                    userPool = new CognitoUserPool(mAuthPoolId, mClientId, IdentityClientProvider);
                }

                return userPool;
            }
        }

        // Identity provider
        private static AmazonCognitoIdentityProviderClient identityClientProvider;
        public static AmazonCognitoIdentityProviderClient IdentityClientProvider
        {
            get
            {
                if (identityClientProvider == null)
                {
                    identityClientProvider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), mRegion);
                }

                return identityClientProvider;
            }
        }    
    }
}

    