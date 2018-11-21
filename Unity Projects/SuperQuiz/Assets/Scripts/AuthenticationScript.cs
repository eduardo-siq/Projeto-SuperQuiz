namespace Firebase.Sample.Auth {
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AuthenticationScript : MonoBehaviour {
	 
	 protected Firebase.Auth.FirebaseAuth auth;
	 
	 Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth = new Dictionary<string, Firebase.Auth.FirebaseUser>();
	 
	 Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
	 
	public virtual void Start() {
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				InitializeFirebase();
			}else {
			Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
			}
		});
	}
	
	// Handle initialization of the necessary firebase modules:
    protected void InitializeFirebase() {
      Debug.Log("Setting up Firebase Auth");
/*      auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
      auth.StateChanged += AuthStateChanged;
      auth.IdTokenChanged += IdTokenChanged;
      // Specify valid options to construct a secondary authentication object.
      if (otherAuthOptions != null &&
          !(String.IsNullOrEmpty(otherAuthOptions.ApiKey) ||
            String.IsNullOrEmpty(otherAuthOptions.AppId) ||
            String.IsNullOrEmpty(otherAuthOptions.ProjectId))) {
        try {
          otherAuth = Firebase.Auth.FirebaseAuth.GetAuth(Firebase.FirebaseApp.Create(
            otherAuthOptions, "Secondary"));
          otherAuth.StateChanged += AuthStateChanged;
          otherAuth.IdTokenChanged += IdTokenChanged;
        } catch (Exception) {
          Debug.Log("ERROR: Failed to initialize secondary authentication object.");
        }
      }
      AuthStateChanged(this, null);
*/    }

}
}
