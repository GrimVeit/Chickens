using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimationModel
{
    //private void HandlerEggDown(Vector3 vector)
    //{
    //    StartCoroutine(BabyChicken_Coroutine(vector));
    //}

    //private IEnumerator BabyChicken_Coroutine(Vector3 vector)
    //{
    //    Debug.Log("пнргош");
    //    Image image = Instantiate(babyChickenPrefab, parentBabyChickens);
    //    image.transform.SetPositionAndRotation(vector, babyChickenPrefab.transform.rotation);

    //    float duration = 2f;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        image.sprite = image.sprite == spriteBabyChicken1 ? spriteBabyChicken2 : spriteBabyChicken1;

    //        yield return new WaitForSeconds(0.2f);

    //        elapsedTime += 0.2f;
    //    }

    //    MoveBabyChicken(image);
    //}

    //private void MoveBabyChicken(Image image)
    //{
    //    image.sprite = spriteBabyChicken1;

    //    image.transform.DOMove(parentChickenMoveTo.position, 0.5f).OnComplete(() => Destroy(image.gameObject));
    //}
}
