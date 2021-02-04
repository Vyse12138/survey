
Get("/data", p =>
{
TMASurvey survey = new TMASurvey
{
    Title = "test title",
    Questions = new List<Question>
  {
  new Question{Body="Q1.  We consider our current overall turnover to be:",IsCompulsory = 1
  ,Items =new List<ItemOfQuestion>
    {
    new ItemOfQuestion{Options = new List<string>
      {"very pool","poor","average" } }
      } },
      new Question{Body="Q2. Overall turnover:",IsCompulsory = 1
      ,Items =new List<ItemOfQuestion>
        {
        new ItemOfQuestion{Options = new List<string>
          {"very pool","poor","average" } },
          new ItemOfQuestion{Options = new List<string>
            {"very pool","poor","average" } }
            } }
      }
};
return Response.AsJson(survey);

{ 
  "title": "test title",
  "questions": 
  [
    {
      "body": "Q1. We consider our current overall turnover to be:",
      "isCompulsory": 1,
      "items": 
      [ 
        { "options": [ "very pool", "poor", "average" ], "answer": null } 
      ]
    },
    {
      "body": "Q2. Overall turnover:",
      "isCompulsory": 1,
      "items": 
      [ 
        { "options": [ "very pool", "poor", "average" ], "answer": null },
        { "options": [ "very pool", "poor", "average" ], "answer": null } 
      ]
    }
  ]
}