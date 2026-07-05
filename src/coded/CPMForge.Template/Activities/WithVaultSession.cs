#nullable enable
using System;
using System.Activities;
using System.Activities.Statements;
using System.Activities.Validation;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CPMForge.Template.Models;
using CPMForge.Template.Shared;

namespace CPMForge.Template.Activities
{
    [Category("CPMForge.Template")]
    [DisplayName("Vault Session")]
    [Description("Opens a Vault session and makes it available to child activities via a typed delegate. Plus edition stub.")]
    public sealed class WithVaultSession : ContinuableAsyncNativeActivity
    {
        [RequiredArgument]
        [Category("Connection")]
        [DisplayName("Host")]
        [Description("Hostname or URL of the vault server (e.g. vault.example.com).")]
        public InArgument<string>? Host { get; set; }

        [Category("Authentication")]
        [DisplayName("Auth Mode")]
        [Description("Authentication method used to connect to the vault (e.g. Password, Token).")]
        public InArgument<AuthMode>? AuthMode { get; set; }

        public override InArgument<bool> ContinueOnError { get; set; } = new(false);

        [Browsable(false)]
        public ActivityAction<VaultSession> Body { get; set; }

        public WithVaultSession()
        {
            Body = new ActivityAction<VaultSession>
            {
                Argument = new DelegateInArgument<VaultSession>("vaultSession"),
                Handler  = new Sequence { DisplayName = "Do" }
            };
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            if (Host?.Expression == null)
                metadata.AddValidationError(new ValidationError(
                    "Host is required.", isWarning: false, propertyName: nameof(Host)));
        }

        protected override Task<Action<NativeActivityContext>?> ExecuteAsync(
            NativeActivityContext context, CancellationToken cancellationToken)
        {
            // Open session, then schedule Body with the session handle:
            //   var session = await OpenSessionAsync(Host.Get(context), cancellationToken);
            //   return ctx => ctx.ScheduleAction(Body, session, OnCompleted, OnFaulted);
            throw new NotImplementedException(
                "CPMForge.Template Plus edition: WithVaultSession is not yet implemented.");
        }
    }
}
