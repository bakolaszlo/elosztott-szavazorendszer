<template>
  <div class="szavazz">
    <h1>{{formData}}</h1>
    
    
  <FormKit
    type="form"
    help="Válasz beküldése">
    <FormKit
  v-model="value"
  type="radio"
  label="Insert kérdés here"
  :options="['Igen','Nem','Passz']"
  help="Kérem válasszon ki egyet!"/>
  
    </FormKit>
  </div>
  <button>
    Send
  </button>
</template>
<script>import { GetData } from "../utils/fetchdata";

export default {
  data() {
    return {
      formData: null,
    }
  },
  async created() {
    this.formData = await GetData("forms/"+this.$route.params.formId);
    console.log(this.formData)
  },
  methods: {
    GetForm(){
      
    }
  },
}
</script>
<style>
@media (min-width: 1024px) {
  .szavazz {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
