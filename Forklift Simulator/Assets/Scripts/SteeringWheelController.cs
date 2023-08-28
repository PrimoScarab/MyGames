using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
   /*
    LogitechGSDK.LogiControllerPropertiesData properties;

    public float xAxes, GasInput, BreakInput, ClutchInput;

    public bool HShift = true;
    bool isInGear;
    public int CurrentGear;

    // Start is called before the first frame update
    void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    // Update is called once per frame
    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            HShifter(rec);
            xAxes = rec.lX / 32768f; //-1 0 1
            

            Debug.Log(CurrentGear);

            if (rec.lY > 0)
            {
                GasInput = 0;
            }
            else if (rec.lY < 0)
            {
                GasInput = rec.lY / -32768f;
            }

            if (rec.lRz > 0)
            {
                BreakInput = 0;
            }
            else if (rec.lRz < 0)
            {
                BreakInput = rec.lRz / -32768f;
            }

            if (rec.rglSlider[0] > 0)
            {
                ClutchInput = 0;
            }
            else if (rec.rglSlider[0] < 0)
            {
                ClutchInput = rec.rglSlider[0] / -32768f;
            }
            else
            {
                print("No Steering Wheel connected");
            }
        }



        void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter)
        {
            for (int i = 0; i < 128; i++)
            {
                if (shifter.rgbButtons[i] == 128)
                {
                    if (ClutchInput > 0.5f)
                    {
                        if (i == 12)
                        {
                            CurrentGear = 1;
                        }
                        else if (i == 13)
                        {
                            CurrentGear = 2;
                        }
                        else if (i == 14)
                        {
                            CurrentGear = 3;
                        }
                        else if (i == 15)
                        {
                            CurrentGear = 4;
                        }
                        else if (i == 16)
                        {
                            CurrentGear = 5;
                        }
                        else if (i == 17)
                        {
                            CurrentGear = 6;
                        }
                        else if (i == 18)
                        {
                            CurrentGear = -1;
                        }
                    }
                }
            }

        }
    }*/
}
