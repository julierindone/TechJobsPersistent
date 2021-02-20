--Part 1
	--all tables imported correctly!

--Part 2
SELECT name
FROM employers
WHERE location = "St. Louis City";


--Part 3 - I found two different ways to find the same data:

SELECT DISTINCT Name, Description 
FROM skills
INNER JOIN jobskills ON skills.Id = jobskills.SkillId
ORDER BY Name;
----------------------------------
SELECT DISTINCT Name, Description
FROM skills
LEFT JOIN jobskills on skills.Id = jobskills.SkillId
WHERE JobId IS NOT NULL
ORDER BY Name;

