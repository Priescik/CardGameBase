using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;
using UnityEngine.SocialPlatforms;

public class HandView : MonoBehaviour
{
    [SerializeField] SplineContainer _splineContainer;
    [SerializeField] float _maxCardSpacing;
    readonly List<CardView> _cards = new();
    float _depthOffset = 0.01f;

    void Start()
    {
        //transform.LookAt(Camera.main.transform.position);
    }

    public IEnumerator AddCard(CardView cardView)
    {
        _cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }
    public CardView RemoveCard(CardInstance cardInstance)
    {
        CardView cardView = _cards.Where(cardView => cardView.CardInstance == cardInstance).FirstOrDefault();
        if (cardView == null) return null;
        _cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(0.15f));
        return cardView;
    }
    private IEnumerator UpdateCardPositions(float duration)
    {
        if (_cards.Count == 0) yield break;
        Spline spline = _splineContainer.Spline;
        float cardSpacing = Mathf.Min(_maxCardSpacing, 1f / _cards.Count);
        float firstCardPosition = 0.5f - (_cards.Count - 1) * cardSpacing / 2;

        for (int i = 0; i < _cards.Count; i++)
        {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Vector3 forward = spline.EvaluateTangent(p);

            Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(-up, forward).normalized, up);

            //Vector3 rotatedPos = transform.rotation * (splinePosition + 0.05f * i * Vector3.back);
            //Quaternion rotatedRot = transform.rotation * rotation;

            _cards[i].transform.DOMove(transform.position + splinePosition + _depthOffset * i * Vector3.back, duration);
            _cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}