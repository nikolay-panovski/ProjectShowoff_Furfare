using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
   /* public enum RumblePattern
    {
        Constant,
        Pulse,
        Linear
    }
    private PlayerInput _playerInput;
    private RumblePattern activeRumblePattern;
    private float rumbleDuration;
    private float pulseDuration;
    private float lowA;
    private float lowStep;
    private float highA;
    private float highStep;
    private float rumbleStep;
    private bool isMotorActive = false;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    public void RumbleConstant(float low, float high, float duration)
    {
        activeRumblePattern = RumblePattern.Constant;
        lowA = low;
        highA = high;
        rumbleDuration = Time.time + duration;
    }
    public void RumblePulse(float low, float high, float burstTime, float duration)
    {
        activeRumblePattern = RumblePattern.Pulse;
        lowA = low;
        highA = high;
        rumbleStep = burstTime;
        pulseDuration = Time.time + burstTime;
        rumbleDuration = Time.time + duration;
        isMotorActive = true;
        var g = GetGamepad();
        g?.SetMotorSpeeds(lowA, highA);
    }
    public void RumbleLinear(float lowStart, float lowEnd, float highStart, float highEnd, float duration)
    {
        activeRumblePattern = RumblePattern.Linear;
        lowA = lowStart;
        highA = highStart;
        lowStep = (lowEnd - lowStart) / duration;
        highStep = (highEnd - highStart) / duration;
        rumbleDuration = Time.time + duration;
    }
    public void StopRumble()
    {
        var gamepad = GetGamepad();
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
    private Gamepad GetGamepad()
    {
        Gamepad gamepad = null;
        foreach (var g in Gamepad.all)
        {
            foreach (var d in _playerInput.devices)
            {
                if (d.deviceId == g.deviceId)
                {
                    gamepad = g;
                    break;
                }
            }
            if (gamepad != null)
            {
                break;
            }
        }
        return gamepad;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > rumbleDuration)
        {
            StopRumble();
            return;
        }
        var gamepad = GetGamepad();
        if (gamepad == null)
            return;

        switch (activeRumblePattern)
        {
            case RumblePattern.Constant:
                gamepad.SetMotorSpeeds(lowA, highA);
                break;
            case RumblePattern.Pulse:
                if (Time.time > pulseDuration)
                {
                    isMotorActive = !isMotorActive;
                    pulseDuration = Time.time + rumbleStep;
                    if (!isMotorActive)
                    {
                        gamepad.SetMotorSpeeds(0, 0);
                    }
                    else
                    {
                        gamepad.SetMotorSpeeds(lowA, highA);
                    }
                }
                break;
            case RumblePattern.Linear:
                gamepad.SetMotorSpeeds(lowA, highA);
                lowA += (lowStep * Time.deltaTime);
                highA += (highStep * Time.deltaTime);
                break;
            default:
                break;
        }
    }*/
}
