<template>
  <div class="szavazz">
    
    
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
</template>
<script>

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
