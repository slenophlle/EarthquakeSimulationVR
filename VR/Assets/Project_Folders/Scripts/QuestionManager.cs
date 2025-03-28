using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Soru ve Cevaplar")]
    public Question[] questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0; 

    [Header("UI ��eleri")]
    public TMP_Text questionText;
    public TMP_Text[] answerTexts;
    public Button[] answerButtons;
    public GameObject quitApp;
    public Button quitButton;
    public TMP_Text scoreText;
    [SerializeField] private GameObject[] UI_Lines;

    void Start()
    {
        quitApp.SetActive(false);
        UpdateScoreUI(); // Skor UI'yi ba�lat
        LoadQuestion();
        scoreText.gameObject.SetActive(false);
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerTexts[i].gameObject.SetActive(true);
                    answerTexts[i].text = currentQuestion.answers[i];
                }
            }

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].onClick.RemoveAllListeners();
                    int answerIndex = i;
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
                }
            }
        }
        else
        {
            EndQuiz();
        }
    }
    public void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            correctAnswers++; // Do�ru cevap say�s�n� art�r
        }
        UpdateScoreUI(); // Skor ekran�n� g�ncelle
        currentQuestionIndex++;
        LoadQuestion();
    }

    void EndQuiz()
    {
        questionText.text = "Tebrikler! T�m sorular� tamamlad�n�z.";
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (var tmptext in answerTexts)
        {
            tmptext.gameObject.SetActive(false);
        }
        foreach(var line in UI_Lines)
        {
            line.SetActive(false);
        }
        scoreText.gameObject.SetActive(true);
        quitApp.SetActive(true);

        // Butona sadece quiz tamamland�ktan sonra Listener ekleniyor
        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(StartEarthquake);
    }

    void StartEarthquake()
    {
        EarthquakeManager.canActive = true;
        Debug.Log("Deprem sim�lasyonu ba�lat�ld�!");
    }

    void UpdateScoreUI()
    {
        scoreText.text = correctAnswers + " Tane Soruyu Do�ru ��aretlediniz!";
    }
}
