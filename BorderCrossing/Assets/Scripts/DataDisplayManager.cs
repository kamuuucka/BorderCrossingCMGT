using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Takes care of displaying the data collected from the players.
/// </summary>
public class DataDisplayManager : MonoBehaviour
{
    #region Exposed Variables

    [Tooltip("Highest number on the graphs range. Default is 10.")] [SerializeField]
    private int ratingRange = 10;

    [Range(0, 1)] [SerializeField] private float scale = 1;

    [Tooltip("Sprite used as an empty field of the graph.")] [SerializeField]
    private Sprite empty;

    [Tooltip("Sprite used as a filled field of the graph.")] [SerializeField]
    private Sprite filled;

    [SerializeField] private GameObject numberField;

    [Tooltip("Colors that will be used in the display graph.")] [SerializeField]
    private ColorPreset colorPreset;

    [Space(10)] [Tooltip("If true, the debug messages will be displayed.")] [SerializeField]
    private bool isDebug = false;

    #endregion

    #region Private Variables

    private readonly List<Dictionary<int, int>> _listOfValuesWithAppearances = new();
    private readonly List<GameObject> _graph = new();
    private int _questionNumber;

    #endregion

    private void OnEnable()
    {
        //Debug for display testing
        // BoundaryDataStorage.NewDataList.Clear();
        //  for (int i = 0; i < 6; i++)
        //  {
        //      BoundaryData boundaryData = new BoundaryData();
        //      boundaryData.data = new List<int>() { 1, 2, 3, 4, 0, 1 };
        //      BoundaryDataStorage.NewDataList.Add(boundaryData);
        //  }
        
        
        if (BoundaryDataStorage.NewDataList == null || BoundaryDataStorage.NewDataList.Count == 0)
        {
            if (isDebug) Debug.LogError("No data available!");
            return;
        }

        for (int i = 0; i < BoundaryDataStorage.NewDataList[0].data.Count; i++)
        {
            _listOfValuesWithAppearances.Add(ReadScores(BoundaryDataStorage.NewDataList, i));
        }

        GenerateDataDisplay(0);
    }

    /// <summary>
    /// Destroys the graph and creates the new one for another question.
    /// </summary>
    private void RegenerateGraph()
    {
        foreach (var field in _graph)
        {
            Destroy(field);
        }

        _graph.Clear();
        GenerateDataDisplay(_questionNumber);
    }

    /// <summary>
    /// Switches to next data set and regenerates the graph.
    /// </summary>
    public void NextQuestion()
    {
        if (_questionNumber + 1 >= BoundaryDataStorage.NewDataList[0].data.Count - 1)
        {
            _questionNumber = BoundaryDataStorage.NewDataList[0].data.Count - 1;
        }
        else
        {
            _questionNumber++;
        }

        RegenerateGraph();
    }

    /// <summary>
    /// Switches to previous data set and regenerates the graph.
    /// </summary>
    public void PreviousQuestion()
    {
        if (_questionNumber - 1 < 0)
        {
            _questionNumber = 0;
        }
        else
        {
            _questionNumber--;
        }

        RegenerateGraph();
    }

    /// <summary>
    /// Generates sprites to display the data visually.
    /// </summary>
    /// <param name="questionNumber">Number of question that the data should be displayed for.</param>
    private void GenerateDataDisplay(int questionNumber)
    {
        for (int i = 0; i < BoundaryDataStorage.NewDataList.Count; i++)
        {
            CreateNumber(i, -1, i+1);
            
            for (int j = 0; j < ratingRange; j++)
            {
                CreateNumber(-1, j, j+1);
                SpriteRenderer fieldSprite = CreateFieldSprite(i, j);  
               
                if (_listOfValuesWithAppearances[questionNumber].Keys.Contains(j) &&
                    _listOfValuesWithAppearances[questionNumber][j] >= (i + 1))
                {
                    fieldSprite.sprite = filled;
                    fieldSprite.color = colorPreset.LoadColorPreset()[j];
                }
                else
                {
                    fieldSprite.sprite = empty;
                }
            }
        }
    }

    private void CreateNumber(int x, int y, int numberToAssign)
    {
        GameObject field = Instantiate(numberField, transform);
        field.transform.localPosition = new Vector3(1 * scale * x, 1 * scale * y, 0);
        field.transform.localRotation = Quaternion.identity;
        field.transform.localScale = new Vector3(scale, scale, 1);
        TMP_Text fieldText = field.GetComponentInChildren<TMP_Text>();
        fieldText.text = $"{numberToAssign}";
    }

    private SpriteRenderer CreateFieldSprite(int x, int y)
    {
        GameObject field = new GameObject("Field");
        field.transform.SetParent(transform);
        field.transform.localPosition = new Vector3(1 * scale * x, 1 * scale * y, 0);
        field.transform.localRotation = Quaternion.identity;
        field.transform.localScale = new Vector3(scale, scale, 1);
        SpriteRenderer fieldSprite = field.AddComponent<SpriteRenderer>();
        _graph.Add(field);
        return fieldSprite;
    }

    /// <summary>
    /// Read the data set uploaded, count how many times it appears, add it into the dictionary.
    /// </summary>
    /// <param name="data">Data that needs to be sorted into the dictionary.</param>
    /// <param name="questionNumber">Number of question that the data should be counted for.</param>
    /// <returns>Dictionary with the number of how many times specific answers appear on given question.</returns>
    private Dictionary<int, int> ReadScores(List<BoundaryData> data, int questionNumber)
    {
        Dictionary<int, int> valueWithNumberOfAppearance = new Dictionary<int, int>();
        foreach (var boundaryData in data)
        {
            var scoreToInt = boundaryData.data[questionNumber];
            if (!valueWithNumberOfAppearance.Keys.Contains(scoreToInt))
            {
                valueWithNumberOfAppearance.Add(scoreToInt, 1);
            }
            else
            {
                valueWithNumberOfAppearance[scoreToInt]++;
            }
        }

        if (isDebug)
        {
            foreach (var valuePair in valueWithNumberOfAppearance)
            {
                Debug.Log($"Value {valuePair.Key} appears {valuePair.Value} times");
            }
        }

        return valueWithNumberOfAppearance;
    }
}