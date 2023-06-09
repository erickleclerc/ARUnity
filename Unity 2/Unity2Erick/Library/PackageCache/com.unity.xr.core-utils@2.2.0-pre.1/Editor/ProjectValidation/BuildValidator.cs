using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_2021_2_OR_NEWER
using PrefabStageUtility = UnityEditor.SceneManagement.PrefabStageUtility;
#else
using PrefabStageUtility = UnityEditor.Experimental.SceneManagement.PrefabStageUtility;
#endif

namespace Unity.XR.CoreUtils.Editor
{
    /// <summary>
    /// Manages <see cref="BuildValidationRule"/> rules for verifying that project settings are compatible
    /// with the features of an installed XR package. 
    /// </summary>
    /// <remarks>
    /// XR packages can implement a set of <see cref="BuildValidationRule"/> objects and call
    /// <see cref="AddRules(BuildTargetGroup, IEnumerable{BuildValidationRule})"/>
    /// to add them to the UNity project validation system. The rules are displayed to
    /// Unity developers in the **XR Plug-in Management** section of the **Project Settings** window.
    ///
    /// See [Project Validation](xref:xr-core-utils-project-validation) for more information.
    /// </remarks>
    [InitializeOnLoad]
    public static class BuildValidator
    {
        static Dictionary<BuildTargetGroup, List<BuildValidationRule>> s_PlatformRules =
            new Dictionary<BuildTargetGroup, List<BuildValidationRule>>();

        internal static Dictionary<BuildTargetGroup, List<BuildValidationRule>> PlatformRules => s_PlatformRules;

        static BuildValidator()
        {
            // Used implicitly. Called when Unity launches the Editor / Player or recompiles scripts.
        }

        /// <summary>
        /// Adds a set of <see cref="BuildValidationRule"/> for a given platform (<see cref="BuildTargetGroup"/>).
        /// </summary>
        /// <param name="group">The platform to which to add these rules.</param>
        /// <param name="rules">The rules to add to the platform.</param>
        public static void AddRules(BuildTargetGroup group, IEnumerable<BuildValidationRule> rules)
        {
            if (s_PlatformRules.TryGetValue(group, out var groupRules))
                groupRules.AddRange(rules);
            else
            {
                groupRules = new List<BuildValidationRule>(rules);
                s_PlatformRules.Add(group, groupRules);
            }
        }

        internal static void GetCurrentValidationIssues(HashSet<BuildValidationRule> failures,
            BuildTargetGroup buildTargetGroup)
        {
            failures.Clear();
            if (!s_PlatformRules.TryGetValue(buildTargetGroup, out var rules))
                return;

            var inPrefabStage = PrefabStageUtility.GetCurrentPrefabStage() != null;
            foreach (var validation in rules)
            {
                // If current scene is prefab isolation do not run scene validation
                if (inPrefabStage && validation.SceneOnlyValidation)
                    continue;

                if (validation.CheckPredicate == null)
                    failures.Add(validation);
                else if (validation.IsRuleEnabled.Invoke() && !validation.CheckPredicate.Invoke())
                    failures.Add(validation);
            }
        }

        /// <summary>
        /// Checks if any member of a given set of types are present in the currently open scenes.
        /// </summary>
        /// <param name="subscribers">The set of types to check on scenes.</param>
        /// <returns><see langword="true"/> if any of the types have been found in the currently open scenes.</returns>
        public static bool HasTypesInSceneSetup(IEnumerable<Type> subscribers)
        {
            if (Application.isPlaying)
                return false;
            
            foreach (var sceneSetup in EditorSceneManager.GetSceneManagerSetup())
            {
                if (!sceneSetup.isLoaded)
                    continue;

                var scene = SceneManager.GetSceneByPath(sceneSetup.path);

                foreach (var go in scene.GetRootGameObjects())
                {
                    if (subscribers.Any(subscriber => go.GetComponentInChildren(subscriber, true)))
                        return true;
                }
            }

            return false;
        }

        internal static bool HasRulesForPlatform(BuildTargetGroup buildTarget)
        {
            return s_PlatformRules.TryGetValue(buildTarget, out _);
        }
    }
}
