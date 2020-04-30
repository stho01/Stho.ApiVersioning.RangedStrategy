using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.Web.Http.Versioning.Conventions;
using Stho.ApiVersioning.RangedStrategy.Annotations;

namespace Stho.ApiVersioning.RangedStrategy.Conventions
{
    public class RangedVersioningControllerConvention : IControllerConvention
    {
        private readonly ApiVersions _versions;

        public RangedVersioningControllerConvention(ApiVersions versions)
        {
            _versions = versions;
        }

        public bool Apply(IControllerConventionBuilder controller, HttpControllerDescriptor controllerDescriptor)
        {
            var state = new StateHolder(controller, controllerDescriptor);

            if (!state.IsValid)
                return false;

            ApplyControllerVersions(state);
            ApplyControllerActionVersions(state);

            return true;
        }

        private void ApplyControllerVersions(StateHolder state)
        {
            var controllerVersions = _versions.Where(x => x >= state.ControllerIntroduced.Version);
            if (state.IsControllerMarkedAsRemoved)
                controllerVersions = controllerVersions.Where(x => x < state.ControllerRemoved.Version);
           
            state.Controller.HasApiVersions(controllerVersions.ToArray());
        }

        private void ApplyControllerActionVersions(StateHolder state)
        {
            foreach (var action in state.ActionDescriptors)
            {
                var introduced = action.GetCustomAttributes<IntroducedInApiVersionAttribute>().SingleOrDefault() ?? state.ControllerIntroduced;
                var removed = action.GetCustomAttributes<RemovedInApiVersionAttribute>().SingleOrDefault() ?? state.ControllerRemoved;
                
                //if (introduced.Version > state.ControllerIntroduced.Version)
                //    continue;

                var versions = _versions.Where(x => x >= introduced.Version);
                if (removed != null)
                    versions = versions.Where(x => x < removed.Version);


                state.Controller.Action(action.ActionName).MapToApiVersions(versions);
            }
        }

        /// <summary>Inner class that holds the scoped state of the apply function.</summary>
        private class StateHolder
        {
            private readonly Lazy<IntroducedInApiVersionAttribute> _controllerIntroduced;
            private readonly Lazy<RemovedInApiVersionAttribute> _controllerRemoved;
            private readonly Lazy<HttpActionDescriptor[]> _actionDescriptors;

            public StateHolder(IControllerConventionBuilder controller, HttpControllerDescriptor descriptor)
            {
                Controller = controller;
                ControllerDescriptor = descriptor;

                // apply lazy loading to properties so that they are loaded and cached when needed
                // instead of right a way.
                _controllerIntroduced = new Lazy<IntroducedInApiVersionAttribute>(
                    () => ControllerDescriptor.GetCustomAttributes<IntroducedInApiVersionAttribute>().SingleOrDefault());
                _controllerRemoved = new Lazy<RemovedInApiVersionAttribute>(
                    () => ControllerDescriptor.GetCustomAttributes<RemovedInApiVersionAttribute>().SingleOrDefault());
                _actionDescriptors = new Lazy<HttpActionDescriptor[]>(
                    () => new ApiControllerActionSelector().GetActionMapping(descriptor)?.SelectMany(x => x).ToArray() ?? Array.Empty<HttpActionDescriptor>());
            }

            public IControllerConventionBuilder Controller { get; }
            public HttpControllerDescriptor ControllerDescriptor { get; }
            public HttpActionDescriptor[] ActionDescriptors => _actionDescriptors.Value;
            public IntroducedInApiVersionAttribute ControllerIntroduced => _controllerIntroduced.Value;
            public RemovedInApiVersionAttribute ControllerRemoved => _controllerRemoved.Value;
            public bool IsValid => ControllerIntroduced != null;
            public bool IsControllerMarkedAsRemoved => ControllerRemoved != null;
        }
    }
}