<template>
  <div class="box1">
     <form @submit.prevent="capturetext" class="c-search" autocomplete="off" name="form1">
          <input 
            v-model.lazy.trim="searchtext" 
            aria-label="Enter your search" 
            type="search" 
            name="search-field" 
            role="searchbox" 
            placeholder="Search"
          />
          <button class="c-glyph searchbuttonunset" name="search-button">
              <span class="x-screen-reader">Search</span>
          </button>
        </form>   
      <div class="resultsummary">
        <h6>Displaying results  {{pageItemCountStart}}-{{pageItemCountLast}} of {{totalitems}}(Page {{currentPage}} of {{maxpages}})</h6>
        <div class="sortby">
            <span> Sort by</span>
            <div data-grid="col-3" class="minwidth" >
              <select class="c-search dropdown2"  v-model="type">
                <option value="" selected>Type: All</option>
                <option value="functionapp">Function App</option>
                <option value="logicapp">Logic App</option>
              </select>
          </div>
        </div>
        <!-- <b-pagination size="md" :total-rows="100" v-model="currentPage" :per-page="10" /> -->
      </div>   
    <div class="contributionitem">

      <!-- <article v-for="sample in this.samples" v-bind:data="sample" v-bind:key="sample.template" > -->
      <article
        v-for="sample in this.currentitems"
        v-bind:data="sample"
        v-bind:key="sample.template"
      >
      <div class ="libraryitem">
        <div class="title"><span class="initial">AR</span>  {{sample.title}}</div>
        <div
          class="metrics"
        >By: azure-sdk | 7500  downloads | Last updated: 12 days ago | Latest version 5.1.0</div>
        <p class="description"  >{{sample.description}}</p>
         <div class="container1">
            Tags :  
            <span class="tag" @click= "tagSelected(sample.type)">
              <app-item-type :type="sample.type" :width="14"></app-item-type>
            </span>
            <span class="tag" v-if="sample.language !== '' && sample.language !== 'na'" @click= "tagSelected(sample.language)">
              <app-item-language :language="sample.language" :width="14"></app-item-language>
            </span>
        <div v-for="tag of tags" :key="tag" @click="tagSelected(tag)">
            <span class="tag" >{{tag}}</span>
         </div>
    </div>
        </div>
      </article>
    </div>
      <PaginationNav
          v-if="showPgination"
          :number-of-pages="maxpages"
          base-url="#"
          v-model="currentPage"
        />
  </div>
</template>
<script>
import PaginationNav from "bootstrap-vue/es/components/pagination-nav/pagination-nav";
import BPagination from "bootstrap-vue/es/components/pagination/pagination";
import AppItemType from "./AppItemType.vue";
import AppItemLanguage from "./AppItemLanguage.vue";

export default {
  components: {
    PaginationNav,
    BPagination,
    AppItemType,
    AppItemLanguage

  },
  data() {
    return {
      currentPage: 1,
      searchtext: "",
      numberOfItemInPage: 5,
        searchtext: '',
        activeText:''
    };
  },
  props: {
    samples: {
      required: true
    },
    initialSearchText: {
      required:false
    }
  },

    created() {
    this.setFilter();
  },
methods:{

  //search
    setFilter()
    {
     if (this.initialSearchText)
     {
      this.searchtext = this.initialSearchText;
     }
     

    },
    capturetext() {
      this.activeText === true
    },

    // end search
    //tags
    tagSelected(tag){
            this.searchtext = tag;
        }
    //end tags
},

  computed: {
    tags(){
      return ["tag1", "blob"];
    },

    showPgination() {
      if (this.maxpages > 1) {
        return true;
      } else {
        return false;
      }
    },
    totalitems() {
      return this.samples.length;
    },
    maxpages() {
      return Math.ceil(this.samples.length / this.numberOfItemInPage) ;
    },
    currentitems() {
      var startindex = (this.currentPage - 1) * this.numberOfItemInPage;
      return this.samples.slice(
        startindex,
        startindex + this.numberOfItemInPage
      );
    },
    pageItemCountStart(){
      return (this.currentPage - 1) * this.numberOfItemInPage + 1;
    },
    pageItemCountLast(){
      return (this.currentPage - 1) * this.numberOfItemInPage  + this.numberOfItemInPage;
    },

  }, 
   filters: {
    ToDisplayType(value) {
      if (!value) { return ''; }
      var type = value.toLowerCase();
      if (type === 'functionapp') {
        return 'Function App';
      } else if (type === 'logicapp') {
        return 'Logic App';
      } else {
      return value;
      }
    }
  },
  watch: {
    searchtext() {
      this.$emit('updateFilterText', this.searchtext)
    },
  } 
};
</script>
<style scoped>
.contributionitem {
  max-width: 1500px;
  margin-top:10px;
  padding-bottom: 0px;
}
.initial
{
   background: #ff0000;
    color: #fff;
    padding: 5px 10px;
    border-radius: 60%;
    font-size: 20px;
}
.resultsummary
{
   display: flex;
   flex-direction: row;
  vertical-align: top;
}
.box {
  margin-bottom: 0;
  border-radius: 0;
  /* margin-left: 180px; */
}
.libraryitem
{

  border-bottom: 1px solid #D8D8D8;
    padding-top:10px;
  padding-bottom: 15px;
  width: 80%;
}

.title {
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  font-size: 24px;
  color: #0072c6;

}
.line-clamp2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;  
}
.metrics {
  font-size: 12px;
  color: #555555;
  margin-bottom: 5px;
}

.description {
  /* top: 5.26%; */
  /* bottom: -5.26%; */
  font-family: "Segoe UI", Tahoma, Geneva, Verdana, sans-serif;
  font-size: 14px;
  color: #000000;
  padding-bottom: 10px;
}
strong {
  color: #0072c6;
}

 /* search Bar */
 .c-search1 input[type='search'] {
  background: transparent;
}
.minwidth{
  min-width: 160px;
}
.c-search{
  max-width: 80%;
  margin: 1rem 0.5rem 0;
  height: 24px;
  font-size: 12px;
}
.c-search input[type='search'] {
  /* border-top: none;
  border-right: none;
  border-left: none; */
  background: white;
  border: 1px solid #BCBCBC;
  border-radius: 3px;
  box-sizing: border-box;
  padding-left: 34px!important;
}
.c-search button {
  height: 21px;
  padding-bottom: 4px;
  padding-top: 4px;
  top:0;
  right:unset;
  left:0;
}
.c-search button:before {
  font-size: 13px;
}
 /* end search Bar */
/* Begin tag */
 .container1
{
    /* border: 1px red solid; */
    display: flex;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-size: 14px;
    line-height: normal;
    color: #555555;
    vertical-align: sub;
    /* flex-direction: column; */
}
.tag
{
    /* border: 1px solid green; */
    /* border: 1px blue solid; */
    color: black;
    text-align: center;
    display: table;
    vertical-align: super;
    margin-right: 20px;
    margin-left: 1px;
    background: #EFEFEF;
    border-radius: 12px;
    padding-left: 5px;
    padding-right: 5px;
    padding-bottom: 3px;
    font-size:12px;
    cursor: pointer;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif

}
/* End tag */
</style>
