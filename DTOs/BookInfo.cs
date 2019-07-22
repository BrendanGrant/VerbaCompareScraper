using System;
using System.Collections.Generic;
using System.Text;

namespace VerbaCompareScraper
{
    public class Metadata
    {
        public string term_id { get; set; }
        public string section_id { get; set; }
        public string pf_id { get; set; }
        public int pf_type { get; set; }
        public bool textbook { get; set; }
    }

    public class Offer
    {
        public string isbn { get; set; }
        public string retailer { get; set; }
        public string item_id { get; set; }
        public string condition { get; set; }
        public object title { get; set; }
        public object currency { get; set; }
        public string merchant { get; set; }
        public object rental_days { get; set; }
        public string retailer_name { get; set; }
        public object metadata { get; set; }
        public object in_cart { get; set; }
        public object selected { get; set; }
        public string total { get; set; }
        public int retailer_order { get; set; }
        public string comments { get; set; }
        public string special_comment { get; set; }
        public bool store_branded { get; set; }
        public string data_source { get; set; }
        public string description { get; set; }
        public object seller_rating { get; set; }
        public string fcondition { get; set; }
        public string fprice { get; set; }
        public object discounted_shipping { get; set; }
        public bool can_checkout { get; set; }
        public object feedback_count { get; set; }
        public object location { get; set; }
        public string price { get; set; }
        public int shipping { get; set; }
        public string variant { get; set; }
        public string id { get; set; }
    }

    public class Book
    {
        public string id { get; set; }
        public string isbn { get; set; }
        public string pf_id { get; set; }
        public string new_item_id { get; set; }
        public string used_item_id { get; set; }
        public string required { get; set; }
        public string[] sort_order { get; set; }
        public string cover_image_url { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public object notes { get; set; }
        public string citation { get; set; }
        public Metadata metadata { get; set; }
        public List<Offer> offers { get; set; }
        public string section_id { get; set; }
        public object db_section_id { get; set; }
        public bool inclusive_access { get; set; }
        public string catalog_id { get; set; }
        public string edition { get; set; }
        public string copyright_year { get; set; }
    }

    public class BookInfo
    {
        public string id { get; set; }
        public string title { get; set; }
        public string catalog_id { get; set; }
        public object continuation { get; set; }
        public string name { get; set; }
        public string instructor { get; set; }
        public object notes { get; set; }
        public object insite_url { get; set; }
        public string no_books_text { get; set; }
        public object no_books_link { get; set; }
        public List<Book> books { get; set; }
    }
}
