using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

[RequireComponent(typeof(TMP_Text))]
public class TextMeshAnimator : MonoBehaviour
{
    [SerializeField] private float _horizontalWobble = 3.3f;
    [SerializeField] private float _verticalWobble = 0.8f;
    [SerializeField] private string _indicator;
    [SerializeField] private Gradient _rainbow;
    [SerializeField] private List<string> _texts;

    private TMP_Text _textMesh;
    private Mesh _mesh;
    private Vector3[] _verticies;
    private List<int> _wordIndexes = new() { 0 };
    private Color[] _colors;

    private string _regex;
    private string _enhancementIndicatorStart;
    private string _enhancementIndicatorEnd;
    const int VerticeCount = 4;


    private void Start()
    {
        _textMesh = GetComponent<TMP_Text>();
        _enhancementIndicatorStart = "<" + _indicator + ">";
        _enhancementIndicatorEnd = "</" + _indicator + ">";
        _regex = _enhancementIndicatorStart + "[a-zA-Z ]*</" + _enhancementIndicatorEnd;
        PrepareText();
    }

    public void PrepareText()
    {
        _texts = _textMesh.text.Split(_enhancementIndicatorEnd).ToList<string>();
        List<string> textToRemove = new();
        foreach (var text in _texts)
        {
            if(!text.Contains(_enhancementIndicatorStart))
             {
                textToRemove.Add(text);   
             }
        }

        foreach (var text in textToRemove)
        {
            _texts.Remove(text);
        }

        _textMesh.text = _textMesh.text.Replace(_enhancementIndicatorEnd, "");
        _textMesh.text = _textMesh.text.Replace(_enhancementIndicatorStart, "");
    }

    private void Update()
    {
        SetMeshProperties();
    }

    protected void GatherData()
    {
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _verticies = _mesh.vertices;
    }

    protected void VerticePerLetter()
    {
        for (var i = 0; i < _textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[i];
            int index = characterInfo.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            for (var j = 0; j < VerticeCount; j++)
            {
                _verticies[index + j] += offset;
            }
        }
    }

    protected void ColorPerWord()
    {
        _colors = _mesh.colors;

        // for (int w = 0; w < _wordIndexes.Count; w++)
        // {
        //     // if (!_texts.Contains(w))
        //     // {
        //     //     continue;
        //     // }

        //     int word = _wordIndexes[w];

        //     for (var i = 0; i < _wordLengths[w]; i++)
        //     {
        //         TMP_CharacterInfo characterInfo = _textMesh.textInfo.characterInfo[word + i];
        //         int index = characterInfo.vertexIndex;

        //         for (int j = 0; j < 4; j++)
        //         {
        //             _colors[index + j] = _rainbow.Evaluate(Mathf.Repeat(Time.time + _verticies[index + j].x * 0.001f, 1f));
        //         }
        //     }
        // }
    }

    /// <summary>
    /// Call this after every modification of the verticies.
    /// </summary>

    protected void SetMeshProperties()
    {
        if(_mesh == null) return;

        _mesh.colors = _colors;
        _mesh.vertices = _verticies;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    private Vector2 Wobble(float time)
    {
        return new Vector2(MathF.Sin(time * _horizontalWobble), Mathf.Cos(time * _verticalWobble));
    }
}
