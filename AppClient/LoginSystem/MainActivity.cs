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

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            /*CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "us-east-1:a205f4ac-4f87-49d6-b913-911e4eccb852", // Your identity pool ID
                RegionEndpoint.USEast1 // Region
            );
            var loggingConfig = AWSConfigs.LoggingConfig;
            loggingConfig.LogMetrics = true;
            loggingConfig.LogResponses = ResponseLoggingOption.Always;
            loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;

            //location of the device running
            AWSConfigs.AWSRegion = "us-east-1";
            //time difference between 
            AWSConfigs.CorrectForClockSkew = true;

            CognitoSyncManager syncManager = new CognitoSyncManager(credentials, RegionEndpoint.USEast1);*/

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

