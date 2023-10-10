using UnityEngine;
using UnityEditor;

public class CustomToolDatabase : EditorWindow
{
    [Header("기본 상수")]
    // 기본 호출 단축키(공백은 필수)
    private const string DEFAULT_HOT_KEY = " " + "%g";
    // 메뉴 이름
    private const string DEFAULT_MENU_NAME = "커스텀 메뉴";
    // 윈도우 이름
    private const string DEFAULT_WINDOW_NAME = "데이터베이스";
    // 구분 자
    private const string DEFAULT_SLASH_SYMBOL = "/";
    // 윈도우 패널 가로 & 세로 사이즈 설정
    // 기본 고정 사이즈는 1024 x 720 이다.
    private const float windowWidth = 1024f;
    private const float windowHeight = 720f;
    

    [Header("기본 변수")]
    private int selectedTab = 0; // 선택된 탭
    private string[] menuTabs =
    {
        "몬스터", "포탑", "아이템"
    };

    // 메뉴 이름 / 창 이름 / 단축키 설정
    [MenuItem(DEFAULT_MENU_NAME + DEFAULT_SLASH_SYMBOL + 
        DEFAULT_WINDOW_NAME + DEFAULT_HOT_KEY)] 

    public static void ShowWindow()
    {
        // 메뉴를 클릭했을 때 데이터베이스 툴 열기
        CustomToolDatabase window = GetWindow<CustomToolDatabase>(DEFAULT_WINDOW_NAME);
        // 윈도우 패널 사이즈 조정
        window.minSize = new Vector2(windowWidth, windowHeight);
        window.maxSize = window.minSize;
    }

    private void OnGUI()
    {
        //// 버튼을 생성하고 버튼의 클릭 이벤트를 처리합니다.
        //if (GUILayout.Button("Click Me"))
        //{
        //    Debug.Log("Button Clicked!");
        //}

        GUI.Label(new Rect(100, 100, 1000, 100), "Hello, Unity!");

        DrawTabs();

        switch (selectedTab)
        {
            // 선택된 탭이 "몬스터"일 경우
            case 0:
                DrawTabMonsters();
                break;
            // 선택된 탭이 "포탑"일 경우
            case 1:
                DrawTabTowers();
                break;
            // 선택된 탭이 "아이템"일 경우
            case 2:
                DrawTabItems();
                break;
        }
    }       // OnGUI()

    // 메뉴 탭을 그리는 함수
    private void DrawTabs()
    {
        // { 아래의 모든 레이아웃은 가로로 나열됨
        GUILayout.BeginHorizontal();

        // menuTabs 길이 만큼 순회
        for (int i = 0; i < menuTabs.Length; i++)
        {
            // 탭을 생성하고 인덱스와 타입을 Button으로 지정
            if (GUILayout.Toggle(selectedTab == i, menuTabs[i], "Button"))
            {
                // 인덱스를 i로 설정
                selectedTab = i;
            }
        }

        // } 위의 모든 레이아웃은 가로로 나열됨
        GUILayout.EndHorizontal();
    }       // DrawTabs()

    // "몬스터" 탭 컨텐츠를 호출하는 함수
    private void DrawTabMonsters()
    {
        GUILayout.Label("Tab 1 Content");
    }       // DrawTabMonsters()

    // "포탑" 탭 컨텐츠를 호출하는 함수
    private void DrawTabTowers()
    {
        GUILayout.Label("Tab 2 Content");
    }       // DrawTabTowers()

    // "아이템" 탭 컨텐츠를 호출하는 함수
    private void DrawTabItems()
    {
        GUILayout.Label("Tab 3 Content");
    }       // DrawTabItems()

}       // Class CustomToolDatabase
