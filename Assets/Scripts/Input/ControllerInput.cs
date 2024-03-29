using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {
	
	private static ControllerInput instance;
	
	public int numberOfControllers;
	
	// The normalized amount a trigger must be down to
	// be considered down as a button
	public float buttonMinimum;
	
	public string A_ButtonFireString;
	public string B_ButtonFireString;
	public string X_ButtonFireString;
	public string Y_ButtonFireString;
	
	public string Left_Dpad_ButtonString;
	public string Right_Dpad_ButtonString;
	public string Up_Dpad_ButtonString;
	public string Down_Dpad_ButtonString;
	
	private IntParamBoolDelegate Left_Dpad_Input_Delegate;
	private IntParamBoolDelegate Right_Dpad_Input_Delegate;
	private IntParamBoolDelegate Up_Dpad_Input_Delegate;
	private IntParamBoolDelegate Down_Dpad_Input_Delegate;

	public string Left_Bumper_ButtonString;
	public string Right_Bumper_ButtonString;
	public string Left_Analog_ButtonString;
	public string Right_Analog_ButtonString;
	
	public string LeftTrigger_AxisString;
	public string RightTrigger_AxisString;
	
	private TriggerStates[] triggerStates;
	
	public string LeftAnalog_X_AxisString;
	public string LeftAnalog_Y_AxisString;
	public string RightAnalog_X_AxisString;
	public string RightAnalog_Y_AxisString;
	
	public string Back_ButtonString;
	public string Start_ButtonString;
	public string Home_ButtonString;
	
	// WINDOWS
	
	public string LeftTrigger_AxisString_Windows;
	public string RightTrigger_AxisString_Windows;
	
	public string LeftAnalog_X_AxisString_Windows;
	public string LeftAnalog_Y_AxisString_Windows;
	public string RightAnalog_X_AxisString_Windows;
	public string RightAnalog_Y_AxisString_Windows;
	
	public string Right_Bumper_ButtonString_Windows;
	public string Left_Bumper_ButtonString_Windows;
	
	public string Left_Dpad_ButtonString_Windows;
	public string Right_Dpad_ButtonString_Windows;
	public string Up_Dpad_ButtonString_Windows;
	public string Down_Dpad_ButtonString_Windows;
	

	void Awake(){
		if(instance == null){
			instance = this;	
			InitTriggerStates();
			LoadDpadDelegates();
			enabled = true;
			
			CheckPlatform();
		}
	}
	
	void CheckPlatform(){
		if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsEditor){
			
			LeftAnalog_X_AxisString = LeftAnalog_X_AxisString_Windows;
			LeftAnalog_Y_AxisString = LeftAnalog_Y_AxisString_Windows;
			RightAnalog_X_AxisString = RightAnalog_X_AxisString_Windows;
			RightAnalog_Y_AxisString = RightAnalog_Y_AxisString_Windows;
			
			LeftTrigger_AxisString = LeftTrigger_AxisString_Windows;
			RightTrigger_AxisString = RightTrigger_AxisString_Windows;
			
//			Right_Bumper_ButtonString = Right_Bumper_ButtonString_Windows;
//			Left_Bumper_ButtonString = Left_Bumper_ButtonString_Windows;
			
			Left_Dpad_ButtonString = Left_Dpad_ButtonString_Windows;
			Right_Dpad_ButtonString = Right_Dpad_ButtonString_Windows;
			Up_Dpad_ButtonString = Up_Dpad_ButtonString_Windows;
			Down_Dpad_ButtonString = Down_Dpad_ButtonString_Windows;

			Left_Dpad_Input_Delegate = Left_Dpad_Axis_Input;
			Right_Dpad_Input_Delegate = Right_Dpad_Axis_Input;
			Up_Dpad_Input_Delegate = Up_Dpad_Axis_Input;
			Down_Dpad_Input_Delegate = Down_Dpad_Axis_Input;
		}
	}
	
	void InitTriggerStates(){
		triggerStates = new TriggerStates[numberOfControllers];
		for(int i = 0; i < triggerStates.Length; i++){
			triggerStates[i] = new TriggerStates();	
		}
	}

	void LoadDpadDelegates(){
		Left_Dpad_Input_Delegate = Left_Dpad_Button_Input;
		Right_Dpad_Input_Delegate = Right_Dpad_Button_Input;
		Up_Dpad_Input_Delegate = Up_Dpad_Button_Input;
		Down_Dpad_Input_Delegate = Down_Dpad_Button_Input;
	}

	// Monitors controller inputs
	void Update(){

		for(int i = 1; i < triggerStates.Length + 1; i++){
			CheckState(i);
		}
	}
	
	void CheckState(int controllerNum){
		
		TriggerStates triggerState = TriggerState(controllerNum);
		
		bool lastHeldRight = triggerState.rightTriggerHeld;
		triggerState.rightTriggerHeld = RightTriggerAbove(controllerNum, buttonMinimum);
		triggerState.rightTriggerDown = !lastHeldRight && triggerState.rightTriggerHeld;
		triggerState.rightTriggerUp = lastHeldRight && !triggerState.rightTriggerHeld;
		
		bool lastHeldLeft = triggerState.leftTriggerHeld;
		triggerState.leftTriggerHeld = LeftTriggerAbove(controllerNum, buttonMinimum);
		triggerState.leftTriggerDown = !lastHeldLeft && triggerState.leftTriggerHeld;
		triggerState.leftTriggerUp = lastHeldLeft && !triggerState.leftTriggerHeld;
	}
	
	TriggerStates TriggerState(int controllerNum){
		return triggerStates[controllerNum - 1];
	}
	
	void LateUpdate(){
		ClearStates();	
	}
	
	void ClearStates(){
		for(int i = 0; i < triggerStates.Length; i++){
			triggerStates[i].Clear();
		}	
	}
	
	public static bool Start_ButtonDown(int controllerNum){
		return ButtonDown(instance.Start_ButtonString, controllerNum);
	}
	
	public static bool Start_Button(int controllerNum){
		return Button(instance.Start_ButtonString, controllerNum);
	}
	
	public static bool A_ButtonDown(int controllerNum){
		return ButtonDown(instance.A_ButtonFireString, controllerNum);	
	}
	
	public static bool A_Button(int controllerNum){
		return Button(instance.A_ButtonFireString, controllerNum);	
	}
	
	public static bool A_ButtonUp(int controllerNum){
		return ButtonUp(instance.A_ButtonFireString, controllerNum);	
	}
	
	public static bool B_ButtonDown(int controllerNum){
		return ButtonDown(instance.B_ButtonFireString, controllerNum);	
	}
	
	public static bool B_Button(int controllerNum){
		return Button(instance.B_ButtonFireString, controllerNum);	
	}
	
	public static bool B_ButtonUp(int controllerNum){
		return ButtonUp(instance.B_ButtonFireString, controllerNum);	
	}
	
	public static bool X_ButtonDown(int controllerNum){
		return ButtonDown(instance.X_ButtonFireString, controllerNum);	
	}
	
	public static bool X_Button(int controllerNum){
		return Button(instance.X_ButtonFireString, controllerNum);	
	}
	
	public static bool X_ButtonUp(int controllerNum){
		return ButtonUp(instance.X_ButtonFireString, controllerNum);	
	}
	
	public static bool Y_ButtonDown(int controllerNum){
		return ButtonDown(instance.Y_ButtonFireString, controllerNum);	
	}
	
	public static bool Y_Button(int controllerNum){
		return Button(instance.Y_ButtonFireString, controllerNum);	
	}
	
	public static bool Y_ButtonUp(int controllerNum){
		return ButtonUp(instance.Y_ButtonFireString, controllerNum);	
	}

	private bool Left_Dpad_Button_Input(int controllerNum){
		return Button(instance.Left_Dpad_ButtonString, controllerNum);
	}

	private bool Right_Dpad_Button_Input(int controllerNum){
		return Button(instance.Right_Dpad_ButtonString, controllerNum);
	}

	private bool Up_Dpad_Button_Input(int controllerNum){
		return Button(instance.Up_Dpad_ButtonString, controllerNum);
	}

	private bool Down_Dpad_Button_Input(int controllerNum){
		return Button(instance.Down_Dpad_ButtonString, controllerNum);
	}

	private bool Left_Dpad_Axis_Input(int controllerNum){
		float value = Axis(instance.Left_Dpad_ButtonString, controllerNum);
		if(value == -1){
			return true;
		}
		else{
			return false;
		}
	}

	private bool Right_Dpad_Axis_Input(int controllerNum){
		float value = Axis(instance.Right_Dpad_ButtonString, controllerNum);
		if(value == 1){
			return true;
		}
		else{
			return false;
		}
	}

	private bool Up_Dpad_Axis_Input(int controllerNum){
		float value = Axis(instance.Up_Dpad_ButtonString, controllerNum);
		if(value == 1){
			return true;
		}
		else{
			return false;
		}
	}
	
	private bool Down_Dpad_Axis_Input(int controllerNum){
		float value = Axis(instance.Down_Dpad_ButtonString, controllerNum);
		if(value == -1){
			return true;
		}
		else{
			return false;
		}
	}

	public static bool Left_Dpad_ButtonDown(int controllerNum){
		return instance.Left_Dpad_Input_Delegate(controllerNum);	
	}
	
	public static bool Left_Dpad_Button(int controllerNum){
		return instance.Left_Dpad_Input_Delegate(controllerNum);
	}
	
	public static bool Left_Dpad_ButtonUp(int controllerNum){
		return ButtonUp(instance.Left_Dpad_ButtonString, controllerNum);	
	}
	
	public static bool Right_Dpad_ButtonDown(int controllerNum){
		return ButtonDown(instance.Right_Dpad_ButtonString, controllerNum);	
	}
	
	public static bool Right_Dpad_Button(int controllerNum){
		return instance.Right_Dpad_Input_Delegate(controllerNum);		
	}
	
	public static bool Right_Dpad_ButtonUp(int controllerNum){
		return ButtonUp(instance.Right_Dpad_ButtonString, controllerNum);	
	}
	public static bool Up_Dpad_ButtonDown(int controllerNum){
		return ButtonDown(instance.Up_Dpad_ButtonString, controllerNum);	
	}
	
	public static bool Up_Dpad_Button(int controllerNum){
		return instance.Up_Dpad_Input_Delegate(controllerNum);		
	}
	
	public static bool Up_Dpad_ButtonUp(int controllerNum){
		return ButtonUp(instance.Up_Dpad_ButtonString, controllerNum);	
	}

	public static bool Down_Dpad_ButtonDown(int controllerNum){
		return ButtonDown(instance.Down_Dpad_ButtonString, controllerNum);	
	}
	
	public static bool Down_Dpad_Button(int controllerNum){
		return instance.Down_Dpad_Input_Delegate(controllerNum);		
	}
	
	public static bool Down_Dpad_ButtonUp(int controllerNum){
		return ButtonUp(instance.Down_Dpad_ButtonString, controllerNum);	
	}
	
	
	public static float LeftAnalog_X_Axis(int controllerNum){
		return Axis(instance.LeftAnalog_X_AxisString, controllerNum);
	}
	
	public static float LeftAnalog_X_Axis(int controllerNum, float deadMagnitude){
		return Axis(instance.LeftAnalog_X_AxisString, controllerNum, deadMagnitude);
	}
	
	public static float LeftAnalog_Y_Axis(int controllerNum){
		return Axis(instance.LeftAnalog_Y_AxisString, controllerNum);
	}
	
	public static float LeftAnalog_Y_Axis(int controllerNum, float deadMagnitude){
		return Axis(instance.LeftAnalog_Y_AxisString, controllerNum, deadMagnitude);
	}
	
	public static float LeftAnalog_Magnitude(int controllerNum){
		return AnalogMagnitude(LeftAnalog_X_Axis(controllerNum), LeftAnalog_Y_Axis(controllerNum));	
	}
	
	public static float RightAnalog_X_Axis(int controllerNum){
		return Axis(instance.RightAnalog_X_AxisString, controllerNum);
	}
	
	public static float RightAnalog_X_Axis(int controllerNum, float deadMagnitude){
		return Axis(instance.RightAnalog_X_AxisString, controllerNum, deadMagnitude);
	}
	
	public static float RightAnalog_Y_Axis(int controllerNum){
		return Axis(instance.RightAnalog_Y_AxisString, controllerNum);
	}
	
	public static float RightAnalog_Y_Axis(int controllerNum, float deadMagnitude){
		return Axis(instance.RightAnalog_Y_AxisString, controllerNum, deadMagnitude);
	}
	
	public static float RightAnalog_Magnitude(int controllerNum){
		return AnalogMagnitude(RightAnalog_X_Axis(controllerNum), RightAnalog_Y_Axis(controllerNum));	
	}
	
	public static bool RightAnalog_ClickDown(int controllerNum){
		return ButtonDown(instance.Right_Analog_ButtonString, controllerNum);
	}
	
	public static bool RightAnalog_Click(int controllerNum){
		return Button(instance.Right_Analog_ButtonString, controllerNum);
	}
	
	public static bool RightAnalog_ClickUp(int controllerNum){
		return ButtonUp(instance.Right_Analog_ButtonString, controllerNum);
	}
	
	public static float LeftTriggerAxis(int controllerNum){
		return TriggerAxis(instance.LeftTrigger_AxisString, controllerNum);
	}
	
	private static bool LeftTriggerAbove(int controllerNum, float minimumPercentDown){
		return TriggerAxis(instance.LeftTrigger_AxisString, controllerNum) > minimumPercentDown;
	}
	
	public static bool LeftTriggerDown(int controllerNum, float minimumPercentDown){
		return instance.TriggerState(controllerNum).leftTriggerDown;
	}
	// removed second parameter as it wasn't being used?
	// , float minimumPercentDown
	public static bool LeftTriggerHeld(int controllerNum){
		return instance.TriggerState(controllerNum).leftTriggerHeld;
	}
	
	public static bool LeftTriggerUp(int controllerNum, float minimumPercentDown){
		return instance.TriggerState(controllerNum).leftTriggerUp;
	}
	
	public static float RightTriggerAxis(int controllerNum){
		return TriggerAxis(instance.RightTrigger_AxisString, controllerNum);
	}
	
	private static bool RightTriggerAbove(int controllerNum, float minimumPercentDown){
		return TriggerAxis(instance.RightTrigger_AxisString, controllerNum) > minimumPercentDown;
	}
	
	public static bool RightTriggerHeld(int controllerNum){
		return instance.TriggerState(controllerNum).rightTriggerHeld;
	}
	
	public static bool RightTriggerDown(int controllerNum){
		return instance.TriggerState(controllerNum).rightTriggerDown;
	}
	
	public static bool RightTriggerUp(int controllerNum){
		return instance.TriggerState(controllerNum).rightTriggerUp;
	}
	//Added bumpers because I didn't see them?
	public static bool RightBumper_Down(int controllerNum){
		return ButtonDown(instance.Right_Bumper_ButtonString, controllerNum);
	}
	
	public static bool LeftBumper_Down(int controllerNum){
		return ButtonDown(instance.Left_Bumper_ButtonString, controllerNum);
	}
	
	private static bool ButtonDown(string buttonName, int controllerNum){
		return Input.GetButtonDown(buttonName + "_" + controllerNum);
	}
	
	private static bool Button(string buttonName, int controllerNum){
		return Input.GetButton(buttonName + "_" + controllerNum);
	}
	
	private static bool ButtonUp(string buttonName, int controllerNum){
		return Input.GetButtonUp(buttonName + "_" + controllerNum);
	}
	
	private static float Axis(string axisName, int controllerNum){
		return Input.GetAxis(axisName + "_" + controllerNum);	
	}
	
	private static float Axis(string axisName, int controllerNum, float deadMagnitude){
		float result = Axis(axisName, controllerNum);
		if(result < deadMagnitude){
			result = 0;	
		}
		return result;
	}
	
	private static float TriggerAxis(string axisName, int controllerNum){
		return (Axis(axisName, controllerNum) + 1) / 2;
	}
	
	private static float AnalogMagnitude(float x, float y){
		return 	Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));	
	}
}
