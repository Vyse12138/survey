
Get("/data", p =>
{
TMASurvey survey = new TMASurvey
{
  Title = "test title",
  Description = "test description",
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
            {"very pool","poor","average" }
        }     
      }
    }
  }
};
return Response.AsJson(survey);


// mock api address:
https://run.mocky.io/v3/878f959c-439a-4e3f-be66-5075fdcc84d8


{ 
  "title": "test title",
  "description": "test description",
  "questions": 
  [
    {
      "id": "1",
      "body": "Q1. Overall turnover:",
      "isCompulsory": 1,
      "items": [
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "shad"
        }
      ]
    },
    {
      "id": "2",
      "body": "Q2. Overall turnover:",
      "isCompulsory": 1,
      "items": [
        {
          "options": [
            "very pool",
            "poor",
            "average",
            "good",
            "very good"
          ],
          "answer": null,
          "name": "shad"
        }
      ]
    },
    {
      "id": "3",
      "body": "Q3. Overall turnover:",
      "isCompulsory": 1,
      "items": [
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "shad"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "bar"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "trap"
        }
      ]
    },
    {
      "id": "4",
      "body": "Q4. Overall turnover:",
      "isCompulsory": 1,
      "items": [
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "shad"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "bar"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "foo"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "cat"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "dog"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "boy"
        }
      ]
    },
    {
      "id": "5",
      "body": "Q5. Overall turnover:",
      "isCompulsory": 1,
      "items": [
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "shad"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "bar"
        },
        {
          "options": [
            "very pool",
            "poor",
            "average"
          ],
          "answer": null,
          "name": "trap"
        }
      ]
    },
    {
      "id": "6",
      "body": "Q6. Overall turnover:",
      "isCompulsory": 0,
      "items": [
        {
          "options": [
            "very pool",
            "poor"
          ],
          "answer": null,
          "name": "shad"
        }
      ]
    },
    {
      "id": "7",
      "body": "Q7. Overall turnover:",
      "isCompulsory": 0,
      "items": [
        {
          "options": [
            "very pool",
            "poor"
          ],
          "answer": null,
          "name": "shad"
        },
        {
          "options": [
            "very pool",
            "poor"
          ],
          "answer": null,
          "name": "bar"
        },
        {
          "options": [
            "very pool",
            "poor"
          ],
          "answer": null,
          "name": "trap"
        }
      ]
    }
  ]
}