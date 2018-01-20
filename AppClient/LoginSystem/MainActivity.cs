using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using Amazon;
using Amazon.CognitoSync.SyncManager;
using Amazon.CognitoSync;
using Amazon.Auth.AccessControlPolicy;
using Amazon.CognitoIdentity;

namespace LoginSystem
{
    [Activity(Label = "LoginSystem", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnSignUp;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // getting credentials
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "IDENTITY_POOL_ID",    // Cognito Identity Pool ID
            RegionEndpoint.USEast1 // Region
            );


            // creating syncmanager
            CognitoSyncManager syncManager = new CognitoSyncManager(
            credentials,
            new AmazonCognitoSyncConfig
            {
            RegionEndpoint = RegionEndpoint.USEast1 // Region
            }
            );

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            mBtnSignUp.Click += (object sender, EventArgs args) =>
                {
                    //Pull up dialog
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    dialog_SignUp signUpDialog = new dialog_SignUp();
                    signUpDialog.Show(transaction, "dialog fragment");

                    signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
                };
        }

        void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {

            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
    

        }
       
        private void ActLikeARequest()
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
            int x = Resource.Animation.slide_right;
        }
    }
}

