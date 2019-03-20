<template>
<div class="sidenav3">
 
    <div id="accordion" class="panel behclick-panel">
        <div class="panel-heading1">
            Filter by
        </div>
     
        <div class="panel-body" >
            <div class="panel-heading" >
                <div data-toggle="collapse" href="#collapse0">
                    Type <i class="indicator fa fa-caret-down" aria-hidden="true"></i> 
                </div>
            </div>
            <div id="collapse0" class="panel-collapse1 collapse in" >
                <ul class="list-group">
                    <li class="list-group-item">
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" value="functionapp" v-model="Types">
                                Function app
                            </label>
                        </div>
                    </li>
                    <li class="list-group-item">
                        <div class="checkbox" >
                            <label>
                                <input type="checkbox" value="logicapp" v-model="Types">
                                Logic app
                            </label>
                        </div>
                    </li>

                </ul>
            </div>
            <div class="panel-heading" >
                    <div data-toggle="collapse" href="#collapse1">
                        Language <i class="indicator fa fa-caret-down" aria-hidden="true"></i> 
                    </div>
            </div>
            		<div id="collapse1" class="panel-collapse collapse in" >
						<ul class="list-group">
							<li class="list-group-item">
								<div class="checkbox">
									<label>
										<input type="checkbox" value="csharp" v-model="Languages">
										c#
									</label>
								</div>
							</li>
							<li class="list-group-item">
								<div class="checkbox" >
									<label>
										<input type="checkbox" value="javascript" v-model="Languages">
										JavaScript
									</label>
								</div>
							</li>
							<li class="list-group-item">
								<div class="checkbox"  >
									<label>
										<input type="checkbox" value="powershell" v-model="Languages">
										Powershell
									</label>
								</div>
							</li>
						</ul>
					</div>
        </div>
    </div>


</div>
</template>
<script>
export default {
  data () {
      return {
        Types: [],
         Languages: []
      };
    },
     props: {
    filters: {
      required:false
    }
  },
created() {
    this.setFilter();
  },
methods:{
 setFilter()
    {
     if (this.filters.languages)
     {
      this.Languages = this.filters.languages;
     }
     if (this.filters.types)
     {
      this.Types = this.filters.types;
     }
    }
},

computed : {
    activeFilters() {
      return {
        languages: this.Languages,
        types: this.Types,
      }
    },
    checkedTypesComputed () {
      return this.Types
    },
     checkedLanguagesComputed () {
      return this.Languages
    }
},
 watch: {
    activeFilters() {
      this.$emit('updateFilters', this.activeFilters)
    },
 },

 
}

</script>
<style lang="scss">
.sidenav3
{
    height: 100%;
    //background: blue;
}
    .filterpaneheading
    {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        font-size: 18px;
        line-height: normal;
        color: #000;
    }
    .panel{
        border: none;
        background-color: inherit;
         height: 100%;
    }
	.behclick-panel  .list-group {
    		margin-bottom: 0px;
            background-color: inherit;
             background-color: #F9F9F9;
	 }

    	.behclick-panel .list-group-item {
    		padding: 5px;
            border:none;
            background-color: #F9F9F9;
		}
		.behclick-panel .panel-heading {

			border-top-right-radius: 0px;
			border-top-left-radius: 0px;
            color: #000;
             text-decoration: none
             
		}

		.behclick-panel {
			border-radius: 0px;
			border-right: 0px;
			border-left: 0px;
			border-bottom: 0px;
			// box-shadow: 0 0px 0px rgba(0, 0, 0, 0);
		}
		.behclick-panel .radio, .checkbox {
			margin: 0px;
			padding-left: 10px;
		}
		  input[type="checkbox"] {
            -webkit-appearance: checkbox !important;
            -moz-appearance: checkbox !important;
            -ms-appearance: checkbox !important;
            -o-appearance: checkbox !important;
            appearance: checkbox !important;
        }
		.behclick-panel .panel-title > a, .panel-title > small, .panel-title > .small, .panel-title > small > a, .panel-title > .small > a {
			outline: none;
		}
		.behclick-panel .panel-body > .panel-heading{
			padding:10px 10px;
		}
		.behclick-panel .panel-body {
			padding: 0px;
		}
</style>
