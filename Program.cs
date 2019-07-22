using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VerbaCompareScraper
{
    class Program
    {
        static string embeddedJavascriptInitStringToLookFor = "new Verba.Compare.Collections.Term";
        static string embeddedJavascriptInitStringToLookFor2 = "new Verba.Compare.Collections.Sections";

        static string rootUrl = "https://insert_your_target_url_here.verbacompare.com";
        static string GetCoursesForTerm(string termid) => $"{rootUrl}/compare/departments/?term={termid}";
        static string GetClassesOfType(string classId, string termId) => $"{rootUrl}//compare/courses/?id={classId}&term_id={termId}";
        static string GetBooksForClassSection(string sectionId) => $"{rootUrl}/comparison?id={sectionId}";
        static string GetSectionsOfClass(string courseId, string termId) => $"{rootUrl}//compare/sections/?id={courseId}&term_id={termId}";
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync(rootUrl);

            var termsListLine = response.Split("\n").Where(l => l.Contains(embeddedJavascriptInitStringToLookFor)).FirstOrDefault().Trim();

            var terms = JsonConvert.DeserializeObject<TermInfo[]>(GetEmbeddedJson(termsListLine, embeddedJavascriptInitStringToLookFor));

            foreach (var term in terms)
            {
                Console.WriteLine(term.name);
                var url = string.Format(GetCoursesForTerm(term.id));
                var courseTypeList = await client.GetStringAsync(url);
                var coursesTypes = JsonConvert.DeserializeObject<CourseTypeInfo[]>(courseTypeList);
                var sectionsToLookUp = new List<string>();
                
                //Uncomment this and modify as you see fit to restrict to specific types of courses
                //coursesTypes = coursesTypes.Where(c => c.name.Contains("CSC")).ToArray(); //Filter to what I care about

                foreach (var courseType in coursesTypes)
                {
                    Console.WriteLine("\t" + courseType.name);
                    var courseUrl = string.Format(GetClassesOfType(courseType.id, term.id));
                    var coursesString = await client.GetStringAsync(courseUrl);
                    var courses = JsonConvert.DeserializeObject<CourseSectionInfo[]>(coursesString);

                    foreach (var course in courses)
                    {
                        Console.WriteLine("\t\t" + course.name);
                        if( course.sections == null)
                        {
                            //There seem to be two different versions out there, one returns sections along with courses, others don't.
                            var sectionUrl = string.Format(GetSectionsOfClass(course.id, term.id));
                            var sectionsString = await client.GetStringAsync(sectionUrl);
                            course.sections = JsonConvert.DeserializeObject<Section[]>(sectionsString);
                        }
                        foreach (var section in course.sections)
                        {
                            Console.WriteLine($"\t\t\t {section.name} - {section.instructor}");

                            //Look up each section one by one, alternatively, can lookup list of sections, comma separated (not implemented in this version)
                            var bookInfoUrl = string.Format(GetBooksForClassSection(section.id));
                            var bookInfoPageText = await client.GetStringAsync(bookInfoUrl);

                            var bookListLine = bookInfoPageText.Split("\n").Where(l => l.Contains(embeddedJavascriptInitStringToLookFor2)).FirstOrDefault()?.Trim();

                            if( bookListLine == null)
                            {
                                continue;
                            }

                            var embeddedString = GetEmbeddedJson(bookListLine, embeddedJavascriptInitStringToLookFor2);
                            var bookInfos = JsonConvert.DeserializeObject<BookInfo[]>(embeddedString);
                            foreach (var bookInfo in bookInfos)
                            {
                                foreach (var book in bookInfo.books)
                                {
                                    Console.WriteLine($"\t\t\t\t{book.title} - {book.author} - {book.isbn}");
                                }
                            }
                        }
                    }
                }
            }
        }

        //Need to find an API which returns this data, so I don't have to do this dirty 'parsing'
        private static string GetEmbeddedJson(string line, string thingToLookFor)
        {
            var start = line.IndexOf(thingToLookFor);

            line = line.Substring(start + thingToLookFor.Length + 1);
            if( line[0] == '(')
            {
                line = line.Substring(1);
            }

            line = line.Substring(0, line.Length - 2);

            return line;
        }
    }
}
