using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputs : MonoBehaviour
{

    public static KeyCode ForwardButton { private set; get; }
    public static KeyCode BackwardButton { private set; get; }
    public static KeyCode LeftButton { private set; get; }
    public static KeyCode RightButton { private set; get; }
    public static KeyCode JumpButton { private set; get; }
    public static KeyCode RunButton { private set; get; }
    public static KeyCode CrouchButton { private set; get; }
    public static KeyCode PauseButton { private set; get; }

    private void Awake()
    {
        SetDefaultKeyCodes();
    }

    private void SetDefaultKeyCodes()
    {
        ForwardButton = KeyCode.W;
        BackwardButton = KeyCode.S;
        LeftButton = KeyCode.A;
        RightButton = KeyCode.D;
        JumpButton = KeyCode.Space;
        RunButton = KeyCode.LeftShift;
        CrouchButton = KeyCode.LeftControl;
        PauseButton = KeyCode.Escape;
    }

    public enum ButtonMask
    {
        Forward = 0b_0000_0001,
        Backward = 0b_0000_0010,
        Left = 0b_0000_0100,
        Right = 0b_0000_1000,
        Jump = 0b_0001_0000,
        Run = 0b_0010_0000,
        Crouch = 0b_0100_0000
    }

    private int SizeOfEnum;
    public enum ButtonArrayIndex
    {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Run,
        Crouch
    }

    private static int[] KeyBoardInputs;


    private void Start()
    {
        SizeOfEnum = ButtonArrayIndex.GetNames(typeof(ButtonArrayIndex)).Length;
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardInputs = UpdateKeyBoardInputsArray();

    }

    private int[] UpdateKeyBoardInputsArray()
    {
        int[] KI = new int[SizeOfEnum];

        KI[(int)ButtonArrayIndex.Forward] = ((Input.GetKey(ForwardButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Backward] = ((Input.GetKey(BackwardButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Left] = ((Input.GetKey(LeftButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Right] = ((Input.GetKey(RightButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Jump] = ((Input.GetKey(JumpButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Run] = ((Input.GetKey(RunButton)) ? 1 : 0);

        KI[(int)ButtonArrayIndex.Crouch] = ((Input.GetKey(CrouchButton)) ? 1 : 0);

        //KeyBoardInputs |= ((Input.GetKey(BackwardButton)) ? ((uint)ButtonMask.Backward) : 0);

        return KI;
    }


    /*

    private uint UpdateKeyBoardInputsMask()
    {
        KeyBoardInputs = 0;

        KeyBoardInputs |= ((Input.GetKey(ForwardButton)) ? ((uint)ButtonMask.Forward) : 0);

        KeyBoardInputs |= ((Input.GetKey(BackwardButton)) ? ((uint)ButtonMask.Backward) : 0);

        KeyBoardInputs |= ((Input.GetKey(LeftButton)) ? ((uint)ButtonMask.Left) : 0);

        KeyBoardInputs |= ((Input.GetKey(RightButton)) ? ((uint)ButtonMask.Right) : 0);

        KeyBoardInputs |= ((Input.GetKey(JumpButton)) ? ((uint)ButtonMask.Jump) : 0);

        KeyBoardInputs |= ((Input.GetKey(RunButton)) ? ((uint)ButtonMask.Run) : 0);

        KeyBoardInputs |= ((Input.GetKey(CrouchButton)) ? ((uint)ButtonMask.Crouch) : 0);

        //KeyBoardInputs |= ((Input.GetKey(BackwardButton)) ? ((uint)ButtonMask.Backward) : 0);


        return KeyBoardInputs;
    }


    public static uint GetKeyBoardInputs(uint InputMask)
    {
        return KeyBoardInputs & InputMask;
    }
    */

    public static int[] GetKeyBoardInputs(uint InputMask)
    {
        List<int> keyBoardInputs = new();
        foreach (uint mask in ButtonMask.GetValues(typeof(ButtonMask)))
        {
            if (CheckIfInInputMask(InputMask, mask))
            {
                keyBoardInputs.Add(1);
            }
        }

        return keyBoardInputs.ToArray();
    }


    public static int[] GetKeyBoardInputs()
    {
        return KeyBoardInputs;
    }

    private static bool CheckIfInInputMask(uint InputMask, uint Check)
    {
        uint value = InputMask & Check;
        if (value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
