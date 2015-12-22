


var MY = MY || {};

viewModelNavBar = (function () {
    var self = this;
    var views = [{ no: 0, view: true, name: "Home" }, { no: 1, view: false, name: "Retrieve all posts" }, { no: 2, view: false, name: "Retrieve a post by ID" }, { no: 3, view: false, name: "Make a new post" }, { no: 4, view: false, name: "Retrieve posts by search word" }];
    var viewNo = 0;
    this.currentView = ko.observableArray(views);
    var showCurrentView = function (no) {
        views[viewNo].view = false;
        viewNo = no;
        views[no].view = true;
        self.currentView.length = 0;
        self.currentView(views);
    }
    return {
        showCurrentView: showCurrentView,
        currentView: self.currentView
    };
});

var dataService = (function () {
    var postUrl = "api/posts/?pageSize=10&page=";

    var getPosts = function (offset, callback) {
        $.getJSON(postUrl+offset, function (data) {
            callback(data)
        })
    },
    getPost = function (id, callback) {
        $.getJSON("api/posts/" +id, function (data) {
            callback(data);
        }).fail(function () {
            alert("No post with that id");
        })
    };

    var newPost = function (title, body, callback) {
        $.post("api/postings/?title=" + title + "&body=" + body, function(data){
            callback(data);
        })
        };

    var getBySearchWord = function (searchWord, callback) {
        $.getJSON("api/SearchStrings/" + searchWord, function (data) {
            callback(data);

        })
    }
    return {
        getPosts: getPosts,
        getPost: getPost,
        getBySearchWord: getBySearchWord,
        newPost : newPost
    }
}());

// ViewModel 2

allPosts = (function () {
    
    var self = this;
    self.elements = ko.observableArray([]);
    this.offSet = 0;
    this.pageNo = ko.observable();
    this.body1 = ko.observable();
    
    this.loadPrevPosts = function () {
        if (offSet > 10) {
            offSet = offSet - 10;
        }
        else {
            offSet = 0;
        }
        this.pageNo(offSet / 10 + 1);
        dataService.getPosts(offSet, function (data) {
               self.elements(data.results);
        })
    }
this.loadNextPosts = function () {
    offSet = offSet + 10;
    this.pageNo(offSet/10+1);

    dataService.getPosts(offSet, function (data) {
        self.elements(data.results);
    })
    }
    this.getTenPosts = function () {
        this.pageNo(1);
        dataService.getPosts(offSet, function (data) {
            self.elements(data.results);
        })
    }
    this.showBody = function (index) {
        self.body1(index);
    }
    return {
        
        elements: self.elements,
        pageNo : self.pageNo,
        bod1 : self.body1
    }
});



// ViewModel 2 end

// ViewModel 3
searchById = (function () {
    var self = this;
    self.searchID = ko.observable();
    self.postById = ko.observable();
    self.IDs = ko.observableArray([]);

    this.loadPostByID = function () {
        contains = false;
        for (i = 0; i < self.IDs().length; i++) {
            if (self.IDs()[i].ID === self.searchID()) {
                contains = true;
            }
        }
        if (!contains) {
            self.IDs.push({ ID: self.searchID() })
        }
        dataService.getPost(self.searchID(), function (post) {
            self.postById(post);
        });
     }   

    this.showPrevIDs = function (index) {
        self.searchID(index);
        self.loadPostByID();
    }
    return {
        loadPostByID: self.loadPostByID,
        IDs : self.IDs
    }
});


// ViewModel 3 end

// ViewModel 4
newPost = (function () {

    var self = this;
    self.title = ko.observable("The title goes here");
    self.body = ko.observable("The body goes here");
    self.posts = ko.observableArray([]);

    this.sendNewPost = function () {

        dataService.newPost(title(), body(), function (data) {
            self.posts.push({ title: self.title(), body: self.body() });
            alert(data.url);
        })
    }
    return {
        posts: self.posts,
        title: self.title,
        body : self.body
    }
});


// ViewModel 4 end

// ViewModel 5

loadSearchString = (function () {

    var self = this;
    self.searchString = ko.observable();
    self.postsRetrieved = ko.observableArray([]);
    self.postsShown = ko.observableArray([]);
    this.searchWords = ko.observableArray([]);
    this.body2 = ko.observable();
    this.searchPost = function () {
        if(searchString() === ""){ alert("You must type one or meore words to search for posts");}
        else {
            contains = false;
        for (i = 0; i < self.searchWords().length; i++) {
            if (self.searchWords()[i].searchedWord === self.searchString()) {
                contains = true;
            }
        }
        if (!contains) {
            self.searchWords.push({ searchedWord: self.searchString() })
        }

        dataService.getBySearchWord(self.searchString(), function (data) {
            self.postsRetrieved(data.results);
            if (self.postsRetrieved().length > 10) {
                self.postsShown(self.postsRetrieved().slice(0, 10));
            }
            else {
                self.postsShown(self.postsRetrieved());
            }
        });
/*        $.getJSON("api/SearchStrings/" + self.searchString(), function (data) {})
            
        
        */
         }
    }
    this.showBodyPost = function (index) {
        self.body2(index);
    }
    this.showSearchWords = function (index) {
        self.searchString(index);
        self.searchPost();
    }
    this.page = 0;

    this.prevShownPosts = function () {
        if (self.postsRetrieved().length > 10) {
            if (page > 10) {
                page = page - 10;
                self.postsShown.length = 0;
                self.postsShown(self.postsRetrieved.slice(page, page + 10));
            }
            else {
                self.postsShown.length = 0;
                self.postsShown(self.postsRetrieved.slice(0, page));
            }
        }
    }

    this.nextToShowPosts = function () {
        if (self.postsRetrieved().length > 10) {
            page = page + 10;
            if (self.postsRetrieved().length > page + 10) {
                self.postsShown.length = 0;
                self.postsShown(self.postsRetrieved.slice(page, page + 10));
            }
            else {
                self.postsShown.length = 0;
                self.postsShown(self.postsRetrieved.slice(self.postsRetrieved.length - 10, self.postsRetrieved.length));
            }
        }
    }
    return {
    postsShown :self.postsShown,
    body2: self.body2,
    postsRetrieved : self.postsRetrieved
    }
});

// ViewModel 5 end

MY.viewModelNavBar = new viewModelNavBar();
MY.allPosts = new allPosts();
MY.searchById = new searchById();
MY.newPost = new newPost();
MY.loadSearchString = new loadSearchString();


ko.applyBindings({
    viewModelNavBar: new viewModelNavBar(),
    allPosts: new allPosts(),
    searchById: new searchById(),
    newPost: new newPost(),
    loadSearchString :new loadSearchString()
});
