using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DataDisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject emptyField;
    [SerializeField] private Sprite filled;
    [SerializeField] private Sprite empty;
    [SerializeField] private int scale = 10;
    [SerializeField] private BoundaryDataList dataList;

    private List<Dictionary<int, int>> _listOfValuesWithAppearances = new List<Dictionary<int, int>>();
    private int _questionNumber;
    private List<GameObject> _graph = new List<GameObject>();

    private void OnEnable()
    {
        for (int i = 0; i < dataList.ReadData()[0].data.Count; i++)
        {
            _listOfValuesWithAppearances.Add(ReadScores(dataList.ReadData(), i));
        }
        GenerateDataDisplay(0);
    }

    public void RegenerateGraph()
    {
        foreach (var field in _graph)
        {
            Destroy(field);
        }
        
        _graph.Clear();
        GenerateDataDisplay(_questionNumber);
    }

    public void NextQuestion()
    {
        if (_questionNumber + 1 >= dataList.ReadData()[0].data.Count-1)
        {
            _questionNumber = dataList.ReadData()[0].data.Count-1;
        }
        else
        {
            _questionNumber++;
        }
    }

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
    }

    private void GenerateDataDisplay(int questionNumber)
    {
        for (int i = 0; i < dataList.ReadData().Count; i++)
        {
            for (int j = 0; j < scale; j++)
            {
                GameObject field = Instantiate(emptyField, new Vector3(1 * j, 1 * i, 0), Quaternion.identity);
                SpriteRenderer fieldSprite = field.GetComponent<SpriteRenderer>();
                _graph.Add(field);
                if (_listOfValuesWithAppearances[questionNumber].Keys.Contains(j) && _listOfValuesWithAppearances[questionNumber][j] >= (i+1))
                {
                    fieldSprite.sprite = filled;
                    fieldSprite.color = Color.red;
                }
                else
                {
                    fieldSprite.sprite = empty;
                }
            }
        }
    }
    
    private Dictionary<int,int> ReadScores(List<BoundaryData> data, int questionNumber)
    {
        Dictionary<int, int> valueWithNumberOfAppearance = new Dictionary<int, int>();
        foreach (var boundaryData in data)
        {
            // if (boundaryData.data[questionNumber] != 0)
            // {
                var scoreToInt = boundaryData.data[questionNumber];
                if (!valueWithNumberOfAppearance.Keys.Contains(scoreToInt))
                {
                    valueWithNumberOfAppearance.Add(scoreToInt, 1);
                }
                else
                {
                    valueWithNumberOfAppearance[scoreToInt]++;
                }
            //}
        }

        foreach (var valuePair in valueWithNumberOfAppearance)
        { 
            Debug.Log($"Value {valuePair.Key} appears {valuePair.Value} times");   
        }

        return valueWithNumberOfAppearance;
    }
}
