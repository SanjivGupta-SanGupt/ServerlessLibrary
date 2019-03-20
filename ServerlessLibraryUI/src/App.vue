<template>
<div class="wrapper5">
    <div data-grid="col-12" class="navbar5">
        <a href="#home">Microsoft Azure</a>
    </div>
    <div class="lower">
        <div data-grid="col-2" class="sidenav5">
            <app-sidebar @updateFilters="updatedFilters = $event" :filters="filters"></app-sidebar>
        </div>
        <div data-grid="col-10" class="main">
            <app-list-item @tagSelected="selectedTag = $event" @updateFilterText="updatedFilterText = $event"  :samples ="list"></app-list-item>
        </div> 
    </div>
</div>
</template>
<script>
  import AppSidebar from './components/AppSidebar.vue'
  import AppListItem from './components/AppListItem.vue'
export default {
 components: {
        AppSidebar,
        AppListItem
    },
    data()
    {
        return {
            filters:{},
            updatedFilters: {},
            samples: [],
            initialSearchText: "",
            updatedFilterText:'',
            selectedTag :'',
        }
    },

    computed:{
  list() {

        var x = this.updatedFilters,   
        filter = new RegExp(this.updatedFilterText, 'i'),
        temp
        temp = this.samples.filter(el =>
          el.title.match(filter)
          || el.description.match(filter)
          || el.authortype.match(filter)
          || el.repository.replace('https://github.com/','').match(filter)
          || el.runtimeversion && el.runtimeversion.match(filter)
          || el.tags && el.tags.some(x => x.match(filter))
        )

      
        if (x.languages && x.languages.length > 0)
        {
           temp = temp.filter(el => x.languages.indexOf(el.language)>-1)

        }

        if (x.types && x.types.length > 0)
        {
           temp = temp.filter(el => x.types.indexOf(el.type)>-1)
        }

        temp = temp.sort(
          function (a, b) {
            return b.totaldownloads - a.totaldownloads;
          });

        return temp
      }
    },
    created() {
         fetch('https://serverlesslibrary.net/api/Library')
        .then(response => response.json())
        .then(data => {
              this.samples = data.sort( function (a, b) {
                                return b.totaldownloads - a.totaldownloads;
                                }
                              );      
          });
      },

 
  watch: {
    selectedTag() {
    //  alert(this.selectedTag);
    },
  } 

};
</script>
<style lang="scss">
html,
body {
  height: 100%;
  width: 100%;
  margin: 0;
}
#app
{
    display: flex;
    flex-direction: column;
    height: 100%;
}
.navbar5 {
  background-color:#1C1C1C;
  width: 100%; 
  box-shadow:0px 2px 8px rgba(0, 0, 0, 0.18);
  min-height: 50px;
}

.sidenav5 {
  height: 100%; 
  box-shadow:0px 0px 1.8px rgba(0, 0, 0, 0.12);
  background: #F9F9F9;;
}

.wrapper5
{
    display: flex;
    flex-direction: column;
    height: 100%;
}
.lower
{
    height: 100%;
}

/* Links inside the navbar */
.navbar a {
  float: left;
  display: block;
  color: #f2f2f2;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

/* Change background on mouse-over */
.navbar a:hover {
  background: #ddd;
  color: black;
}

.main{
    padding-left: 10px;
}


</style>
