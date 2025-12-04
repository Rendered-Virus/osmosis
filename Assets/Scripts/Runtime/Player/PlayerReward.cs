using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerReward : MonoBehaviour
{
   [SerializeField] private GameObject _rewardParticles;
   [SerializeField] private TextMeshProUGUI _rewardText;
   [SerializeField] private AudioClip _rewardSfx;

   public void Reward(string reward)
   {
      Instantiate(_rewardParticles, transform.position, Quaternion.identity);
      AudioManager.Instance.PlaySfxWithPitchShift(_rewardSfx,0.05f,0.01f);
      _rewardText.gameObject.SetActive(true);
      _rewardText.DOFade(1.0f, 0.5f).SetEase(Ease.OutBack);
      _rewardText.text = reward;
      
      Invoke(nameof(FadeOut),2);
   }

   private void FadeOut()
   {
      _rewardText.DOFade(0f, 1);
   }
}
