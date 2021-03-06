﻿public abstract class GamePlatform
{
    // 1) find files no matter if they are in Data\Local\ or Data\Public\
    // 2) find files no matter if game is in debugger, or installed
    // 3) find files no matter if they end in .png or .jpg
    // Returns URL in JavaScript
    public abstract string GetFullFilePath(string filename, BoolRef found);
    public abstract int FloatToInt(float value);
    public abstract string[] StringSplit(string value, string separator, IntRef returnLength);
    public abstract int IntParse(string value);
    public abstract float FloatParse(string value);
    public abstract float MathSqrt(float value);
    public abstract string StringTrim(string value);
    public abstract string IntToString(int value);
    public abstract string Timestamp();
    public abstract string StringFormat(string format, string arg0);
    public abstract string StringFormat2(string format, string arg0, string arg1);
    public abstract string StringFormat3(string format, string arg0, string arg1, string arg2);
    public abstract string StringFormat4(string format, string arg0, string arg1, string arg2, string arg3);
    public abstract void ClipboardSetText(string s);
    public abstract TextTexture CreateTextTexture(string text, float fontSize);
    public abstract void TextSize(string text, float fontSize, IntRef outWidth, IntRef outHeight);
    public abstract void Exit();
    public abstract int[] StringToCharArray(string s, IntRef length);
    public abstract string CharArrayToString(int[] charArray, int length);
    public abstract string PathSavegames();
    public abstract string PathCombine(string part1, string part2);
    public abstract string[] DirectoryGetFiles(string path, IntRef length);
    public abstract string[] FileReadAllLines(string path, IntRef length);
    public abstract void WebClientDownloadDataAsync(string url, HttpResponseCi response);
    public abstract string FileName(string fullpath);
    public abstract void AddOnNewFrame(NewFrameHandler handler);
    public abstract void AddOnKeyEvent(KeyEventHandler handler);
    public abstract void AddOnMouseEvent(MouseEventHandler handler);
    public abstract void AddOnTouchEvent(TouchEventHandler handler);
    public abstract int GetCanvasWidth();
    public abstract int GetCanvasHeight();
    public abstract void GlViewport(int x, int y, int width, int height);
    public abstract void GlClearColorBufferAndDepthBuffer();
    public abstract void GlDisableDepthTest();
    public abstract void BindTexture2d(int texture);
    public abstract Model CreateModel(ModelData modelData);
    public abstract void DrawModel(Model model);
    public abstract void InitShaders();
    public abstract void SetMatrixUniforms(float[] pMatrix, float[] mvMatrix);
    public abstract void GlClearColorRgbaf(float r, float g, float b, float a);
    public abstract void GlEnableDepthTest();
    public abstract int LoadTextureFromFile(string fullPath);
    public abstract string GetLanguageIso6391();
    public abstract int TimeMillisecondsFromStart();
    public abstract void DrawModels(Model[] model, int count);
    public abstract void GlDisableCullFace();
    public abstract void GlEnableCullFace();
    public abstract void ThrowException(string message);
    public abstract void DeleteModel(Model model);
    public abstract void GlEnableTexture2d();
    public abstract BitmapCi BitmapCreate(int width, int height);
    public abstract void BitmapSetPixelsArgb(BitmapCi bmp, int[] pixels);
    public abstract int LoadTextureFromBitmap(BitmapCi bmp);
    public abstract void GLLineWidth(int width);
    public abstract void GLDisableAlphaTest();
    public abstract void GLEnableAlphaTest();
    public abstract void GLDeleteTexture(int id);
    public abstract BitmapCi CreateTextTexture2(Text_ t);
    public abstract float BitmapGetWidth(BitmapCi bmp);
    public abstract float BitmapGetHeight(BitmapCi bmp);
    public abstract void BitmapDelete(BitmapCi bmp);
    public abstract void DrawModelData(ModelData data);
    public abstract bool FloatTryParse(string s, FloatRef ret);
    public abstract float MathCos(float a);
    public abstract float MathSin(float a);
    public abstract void AudioPlay(string path, float x, float y, float z);
    public abstract void AudioPlayLoop(string path, bool play, bool restart);
    public abstract void AudioUpdateListener(float posX, float posY, float posZ, float orientX, float orientY, float orientZ);
    public abstract void ConsoleWriteLine(string p);
    public abstract DummyNetOutgoingMessage CastToDummyNetOutgoingMessage(INetOutgoingMessage message);
    public abstract MonitorObject MonitorCreate();
    public abstract void MonitorEnter(MonitorObject monitorObject);
    public abstract void MonitorExit(MonitorObject monitorObject);
    public abstract bool EnetAvailable();
    public abstract EnetHost EnetCreateHost();
    public abstract void EnetHostInitializeServer(EnetHost host, int port, int peerLimit);
    public abstract bool EnetHostService(EnetHost host, int timeout, EnetEventRef enetEvent);
    public abstract bool EnetHostCheckEvents(EnetHost host, EnetEventRef event_);
    public abstract EnetPeer EnetHostConnect(EnetHost host, string hostName, int port, int data, int channelLimit);
    public abstract void EnetPeerSend(EnetPeer peer, byte channelID, byte[] data, int dataLength, int flags);
    public abstract EnetNetConnection CastToEnetNetConnection(INetConnection connection);
    public abstract EnetNetOutgoingMessage CastToEnetNetOutgoingMessage(INetOutgoingMessage msg);
    public abstract void EnetHostInitialize(EnetHost host, IPEndPointCi address, int peerLimit, int channelLimit, int incomingBandwidth, int outgoingBandwidth);
    public abstract void SaveScreenshot();
    public abstract BitmapCi GrabScreenshot();
    public abstract AviWriterCi AviWriterCreate();
    public abstract bool StringEmpty(string data);
    public abstract float FloatModulo(float a, int b);
    public abstract void SetFreeMouse(bool value);
    public abstract UriCi ParseUri(string uri);
    public abstract OptionsCi LoadOptions();
    public abstract void SaveOptions(OptionsCi options);
    public abstract bool StringContains(string a, string b);
    public abstract RandomCi RandomCreate();
    public abstract void GlClearDepthBuffer();
    public abstract string PathStorage();
    public abstract string StringReplace(string s, string from, string to);
    public abstract PlayerInterpolationState CastToPlayerInterpolationState(InterpolatedObject a);
    public abstract void GlLightModelAmbient(int r, int g, int b);
    public abstract float MathAcos(float p);
    public abstract void SetVSync(bool enabled);
    public abstract string GetGameVersion();
    public abstract void GlEnableFog();
    public abstract void GlHintFogHintNicest();
    public abstract void GlFogFogModeExp2();
    public abstract void GlFogFogColor(int r, int g, int b, int a);
    public abstract void GlFogFogDensity(float density);
    public abstract byte[] GzipDecompress(byte[] compressed, int compressedLength);
    public abstract bool ChatLog(string servername, string p);
    public abstract float MathTan(float p);
    public abstract bool IsValidTypingChar(int c);
    public abstract bool StringStartsWithIgnoreCase(string a, string b);
    public abstract int StringIndexOf(string s, string p);
    public abstract void WindowExit();
    public abstract void MessageBoxShowError(string text, string caption);
    public abstract int ByteArrayLength(byte[] arr);
    public abstract BitmapCi BitmapCreateFromPng(byte[] data, int dataLength);
    public abstract void BitmapGetPixelsArgb(BitmapCi bitmap, int[] bmpPixels);
    public abstract string StringFromUtf8ByteArray(byte[] value, int valueLength);
    public abstract string[] ReadAllLines(string p, IntRef retCount);
    public abstract bool ClipboardContainsText();
    public abstract string ClipboardGetText();
    public abstract void SetTitle(string applicationname);
    public abstract bool Focused();
    public abstract void AddOnCrash(OnCrashHandler handler);
    public abstract string KeyName(int key);
    public abstract DisplayResolutionCi[] GetDisplayResolutions(IntRef resolutionsCount);
    public abstract WindowState GetWindowState();
    public abstract void SetWindowState(WindowState value);
    public abstract void ChangeResolution(int width, int height, int bitsPerPixel, float refreshRate);
    public abstract DisplayResolutionCi GetDisplayResolutionDefault();
    public abstract byte[] StringToUtf8ByteArray(string s, IntRef retLength);
    public abstract void WebClientUploadDataAsync(string url, byte[] data, int dataLength, HttpResponseCi response);
    public abstract string FileOpenDialog(string extension, string extensionName, string initialDirectory);
}

public enum WindowState
{
    Normal,
    Minimized,
    Maximized,
    Fullscreen
}

public class OnCrashHandler
{
    public virtual void OnCrash() { }
}

public abstract class RandomCi
{
    public abstract float NextFloat();
    public abstract int Next();
}

public class OptionsCi
{
    public OptionsCi()
    {
        float one = 1;
        Shadows = false;
        Font = 0;
        DrawDistance = 256;
        UseServerTextures = true;
        EnableSound = true;
        Framerate = 0;
        Resolution = 0;
        Fullscreen = false;
        Smoothshadows = true;
        BlockShadowSave = one * 6 / 10;
        Keys = new int[256];
    }
    internal bool Shadows;
    internal int Font;
    internal int DrawDistance;
    internal bool UseServerTextures;
    internal bool EnableSound;
    internal int Framerate;
    internal int Resolution;
    internal bool Fullscreen;
    internal bool Smoothshadows;
    internal float BlockShadowSave;
    internal int[] Keys;
}

public class UriCi
{
    internal string url;
    internal string ip;
    internal int port;
    internal DictionaryStringString get;
    public string GetUrl() { return url; }
    public string GetIp() { return ip; }
    public int GetPort() { return port; }
    public DictionaryStringString GetGet() { return get; }
}

public class EnetHost
{
}

public abstract class EnetEvent
{
    public abstract EnetEventType Type();
    public abstract EnetPeer Peer();
    public abstract EnetPacket Packet();
}

public class EnetEventRef
{
    internal EnetEvent e;
}

public enum EnetEventType
{
    None,
    Connect,
    Disconnect,
    Receive
}

public class EnetPacketFlags
{
    public const int None = 0;
    public const int Reliable = 1;
    public const int Unsequenced = 2;
    public const int NoAllocate = 4;
    public const int UnreliableFragment = 8;
}

public abstract class EnetPeer
{
    public abstract int UserData();
    public abstract void SetUserData(int value);
    public abstract IPEndPointCi GetRemoteAddress();
}

public abstract class EnetPacket
{
    public abstract int GetBytesCount();
    public abstract byte[] GetBytes();
    public abstract void Dispose();
}

public class MonitorObject
{
}

public class FloatRef
{
    public static FloatRef Create(float value_)
    {
        FloatRef f = new FloatRef();
        f.value = value_;
        return f;
    }
    internal float value;
}

public class KeyEventArgs
{
    int keyCode;
    public int GetKeyCode() { return keyCode; }
    public void SetKeyCode(int value) { keyCode = value; }
}

public class KeyPressEventArgs
{
    int keyChar;
    public int GetKeyChar() { return keyChar; }
    public void SetKeyChar(int value) { keyChar = value; }
}

public class GlKeys
{
    public const int Unknown = 0;
    public const int LShift = 1;
    public const int ShiftLeft = 1;
    public const int RShift = 2;
    public const int ShiftRight = 2;
    public const int LControl = 3;
    public const int ControlLeft = 3;
    public const int RControl = 4;
    public const int ControlRight = 4;
    public const int AltLeft = 5;
    public const int LAlt = 5;
    public const int AltRight = 6;
    public const int RAlt = 6;
    public const int WinLeft = 7;
    public const int LWin = 7;
    public const int RWin = 8;
    public const int WinRight = 8;
    public const int Menu = 9;
    public const int F1 = 10;
    public const int F2 = 11;
    public const int F3 = 12;
    public const int F4 = 13;
    public const int F5 = 14;
    public const int F6 = 15;
    public const int F7 = 16;
    public const int F8 = 17;
    public const int F9 = 18;
    public const int F10 = 19;
    public const int F11 = 20;
    public const int F12 = 21;
    public const int F13 = 22;
    public const int F14 = 23;
    public const int F15 = 24;
    public const int F16 = 25;
    public const int F17 = 26;
    public const int F18 = 27;
    public const int F19 = 28;
    public const int F20 = 29;
    public const int F21 = 30;
    public const int F22 = 31;
    public const int F23 = 32;
    public const int F24 = 33;
    public const int F25 = 34;
    public const int F26 = 35;
    public const int F27 = 36;
    public const int F28 = 37;
    public const int F29 = 38;
    public const int F30 = 39;
    public const int F31 = 40;
    public const int F32 = 41;
    public const int F33 = 42;
    public const int F34 = 43;
    public const int F35 = 44;
    public const int Up = 45;
    public const int Down = 46;
    public const int Left = 47;
    public const int Right = 48;
    public const int Enter = 49;
    public const int Escape = 50;
    public const int Space = 51;
    public const int Tab = 52;
    public const int Back = 53;
    public const int BackSpace = 53;
    public const int Insert = 54;
    public const int Delete = 55;
    public const int PageUp = 56;
    public const int PageDown = 57;
    public const int Home = 58;
    public const int End = 59;
    public const int CapsLock = 60;
    public const int ScrollLock = 61;
    public const int PrintScreen = 62;
    public const int Pause = 63;
    public const int NumLock = 64;
    public const int Clear = 65;
    public const int Sleep = 66;
    public const int Keypad0 = 67;
    public const int Keypad1 = 68;
    public const int Keypad2 = 69;
    public const int Keypad3 = 70;
    public const int Keypad4 = 71;
    public const int Keypad5 = 72;
    public const int Keypad6 = 73;
    public const int Keypad7 = 74;
    public const int Keypad8 = 75;
    public const int Keypad9 = 76;
    public const int KeypadDivide = 77;
    public const int KeypadMultiply = 78;
    public const int KeypadMinus = 79;
    public const int KeypadSubtract = 79;
    public const int KeypadAdd = 80;
    public const int KeypadPlus = 80;
    public const int KeypadDecimal = 81;
    public const int KeypadEnter = 82;
    public const int A = 83;
    public const int B = 84;
    public const int C = 85;
    public const int D = 86;
    public const int E = 87;
    public const int F = 88;
    public const int G = 89;
    public const int H = 90;
    public const int I = 91;
    public const int J = 92;
    public const int K = 93;
    public const int L = 94;
    public const int M = 95;
    public const int N = 96;
    public const int O = 97;
    public const int P = 98;
    public const int Q = 99;
    public const int R = 100;
    public const int S = 101;
    public const int T = 102;
    public const int U = 103;
    public const int V = 104;
    public const int W = 105;
    public const int X = 106;
    public const int Y = 107;
    public const int Z = 108;
    public const int Number0 = 109;
    public const int Number1 = 110;
    public const int Number2 = 111;
    public const int Number3 = 112;
    public const int Number4 = 113;
    public const int Number5 = 114;
    public const int Number6 = 115;
    public const int Number7 = 116;
    public const int Number8 = 117;
    public const int Number9 = 118;
    public const int Tilde = 119;
    public const int Minus = 120;
    public const int Plus = 121;
    public const int LBracket = 122;
    public const int BracketLeft = 122;
    public const int BracketRight = 123;
    public const int RBracket = 123;
    public const int Semicolon = 124;
    public const int Quote = 125;
    public const int Comma = 126;
    public const int Period = 127;
    public const int Slash = 128;
    public const int BackSlash = 129;
    public const int LastKey = 130;
}

public abstract class NewFrameHandler
{
    public abstract void OnNewFrame(NewFrameEventArgs args);
}

public abstract class ImageOnLoadHandler
{
    public abstract void OnLoad();
}

public abstract class KeyEventHandler
{
    public abstract void OnKeyDown(KeyEventArgs e);
    public abstract void OnKeyPress(KeyPressEventArgs e);
    public abstract void OnKeyUp(KeyEventArgs e);
}

public class MouseEventArgs
{
    int x;
    int y;
    int movementX;
    int movementY;
    int button;
    public int GetX() { return x; } public void SetX(int value) { x = value; }
    public int GetY() { return y; } public void SetY(int value) { y = value; }
    public int GetMovementX() { return movementX; } public void SetMovementX(int value) { movementX = value; }
    public int GetMovementY() { return movementY; } public void SetMovementY(int value) { movementY = value; }
    public int GetButton() { return button; } public void SetButton(int value) { button = value; }
}

public class MouseWheelEventArgs
{
    int delta;
    float deltaPrecise;
    public int GetDelta() { return delta; } public void SetDelta(int value) { delta = value; }
    public float GetDeltaPrecise() { return deltaPrecise; } public void SetDeltaPrecise(float value) { deltaPrecise = value; }
}

public class MouseButtonEnum
{
    public const int Left = 0;
    public const int Middle = 1;
    public const int Right = 2;
}

public abstract class MouseEventHandler
{
    public abstract void OnMouseDown(MouseEventArgs e);
    public abstract void OnMouseUp(MouseEventArgs e);
    public abstract void OnMouseMove(MouseEventArgs e);
    public abstract void OnMouseWheel(MouseWheelEventArgs e);
}

public class TouchEventArgs
{
    int x;
    int y;
    int id;
    public int GetX() { return x; } public void SetX(int value) { x = value; }
    public int GetY() { return y; } public void SetY(int value) { y = value; }
    public int GetId() { return id; } public void SetId(int value) { id = value; }
}

public abstract class TouchEventHandler
{
    public abstract void OnTouchStart(TouchEventArgs e);
    public abstract void OnTouchMove(TouchEventArgs e);
    public abstract void OnTouchEnd(TouchEventArgs e);
}

public class NewFrameEventArgs
{
    float dt;
    public float GetDt()
    {
        return dt;
    }
    public void SetDt(float p)
    {
        this.dt = p;
    }
}

public abstract class Texture
{
}

public enum TextAlign
{
    Left,
    Center,
    Right
}

public enum TextBaseline
{
    Top,
    Middle,
    Bottom
}

public class IntRef
{
    public static IntRef Create(int value_)
    {
        IntRef intref = new IntRef();
        intref.value = value_;
        return intref;
    }
    internal int value;
    public int GetValue() { return value; }
    public void SetValue(int value_) { value = value_; }
}
