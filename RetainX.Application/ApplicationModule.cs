using Autofac;
using RetainX.Application.Interfaces;

namespace RetainX.Application
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register PredictionService as a singleton
            builder.RegisterType<PredictionService>().As<IPredictionService>().InstancePerLifetimeScope();

            // Register ModelService as IModelService with Singleton lifetime
            builder.RegisterType<ModelService>().As<IModelService>().SingleInstance();

            builder.RegisterType<ModelTrainingService>().As<IModelTrainingService>().InstancePerLifetimeScope();

            builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerLifetimeScope();
        }
    }
}
