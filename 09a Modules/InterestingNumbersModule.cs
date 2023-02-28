using Autofac;

namespace AD09
{
    public sealed class InterestingNumbersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterestingNumbersGenerator>().As<IInterestingNumbersGenerator>();
        }
    }
}
