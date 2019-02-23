<template>
    <div class="Contribute">
        <span v-if="isAuthenticated">{{userName}}</span>
        <a v-if="!isAuthenticated" href="/api/Library/login" target="_blank">login here</a> 
        <!-- <button @click="addLibraryItem">Submit Contribution</button> -->
  <div class="hero-body">
<div class="container">

  <h1 class="title has-text-centered">Library Item</h1>
  <div class="box">

    <form v-if="isAuthenticated" id="signup-form" @submit.prevent="addLibraryItem">

      <div class="field">
        <label class="label">Title</label>
        <input type="text" class="input"  v-model="title">
      </div>

      <div class="field">
        <label class="label">Template</label>
        <input type="text" class="input" name="Template" v-model="template">
      </div>
      <div class="field">
        <label class="label">Repository</label>
        <input type="text" class="input" name="Repository" v-model="repository">
      </div>
      <div class="field">
        <label class="label">Description</label>
        <input type="text" class="input"  v-model="description">
      </div>
      <div class="field">
        <label class="label">Tags</label>
        <input type="text" class="input"  v-model="tags">
      </div>
      <div class="field">
        <label class="label">Type</label>

        <select  v-model="type">
          <option value="functionapp"  >Function App</option>
          <option value="logicapp" selected>Logic App</option>
        </select>
      </div>
      <div class="field">
        <label class="label">Author</label>
        <input type="text" class="input"  v-model="author">
      </div>
        <div class="field">
        <label class="label">Language</label>
         <select   v-model="language">
          <option value="c#" >Csharp</option>
          <option value="javscript" selected>JavaScript</option>
        </select>
      </div>

         <div class="field">
        <label class="label">AuthorType</label>
        <input type="text" class="input"  v-model="authorType">
      </div>
      <div class="field">
        <label class="label">AuthorTypeDesc</label>
        <input type="text" class="input"  v-model="authortypedesc">
      </div>
      <div class="field">
        <label class="label">RuntimeVersion</label>
         <select   v-model="runtimeVersion">
          <option value="v1" >V1</option>
          <option value="v2" selected>V2</option>
        </select>
      </div>
    
      <!-- submit button -->
      <div class="field has-text-right">
        <button type="submit" >Submit</button>
      </div>
    </form>
    <h1 v-if="issuccessSubmit" class="title has-text-centered">Library item added</h1>

  </div>

</div>
</div>

    </div>
</template>
<script>
export default {
  data () {
      return {
        issuccessSubmit:false,
        isAuthenticated: false,
        userName: null,
        title:"This is a sample function",
        template:"https://raw.githubusercontent.com/anthonychu1/azure-functions-openalpr/master/azuredeploy.json",
        repository:"https://github.com/anthonychu1/azure-functions-openalpr",
        description:"",
        tags:"",
        type:"functionapp",
        language:"c#",
        author:"",
        authorType:"Community",
        authortypedesc:"This is a community contribution",
        runtimeVersion:"v2",
      };
    },

    methods: {
      addLibraryItem() {
        
         fetch('http://localhost:16743/api/contribution/login?returnUrl=http://localhost:8080/#/Contribute', {method:'GET'})
        .then(response => response.json())
        .then(data => {
              this.isAuthenticated = true;
              this.userName = data; 
          });
      },
    addLibraryItem1() {
      this.issuccessSubmit = false;
      var libraryItem = this.getLibraryItem();
      fetch("http://localhost:16743/api/contribution", {
      method: 'put',
      credentials:'include',
			headers: {'Content-Type':'application/json; charset=utf-8'},
			body: JSON.stringify(libraryItem)
    }).
    then(res => {
        if(res.ok) {
          this.issuccessSubmit = true;
          return res;
        } else {
         alert(`Request rejected with status ${res.status}`);
    }
		}).catch(console.error)
    },

    getLibraryItem() {
      var libraryItem = {};
      libraryItem.title = this.libraryItem;
      libraryItem.template = this.template;
      libraryItem.repository = this.repository;
      libraryItem.description = this.description;
      var tagArray = this.tags.split(",");
      libraryItem.tags = tagArray.map(i=>i.trim());
      libraryItem.language = this.language;
      libraryItem.type = this.type;
      libraryItem.author = this.author;
      libraryItem.authorType = this.authorType;
      libraryItem.authortypedesc = this.authortypedesc;
      libraryItem.runtimeVersion = this.runtimeVersion;
      return libraryItem;
    },
    },
  

   created() {
         fetch('http://localhost:16743/api/contribution/AccountName', {method:'GET', credentials:'include', headers:{  'Accept': 'application/json',
            'Content-Type': 'application/json'}})
        .then(response => response.json())
        .then(data => {
              this.isAuthenticated = true;
              this.userName = data; 
          });
      },

};
</script>