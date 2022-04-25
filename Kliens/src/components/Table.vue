<template>
  <div>
    <table
      id="tableComponent"
      class="table table-bordered table-striped"
      v-if="!isResult"
    >
      <thead>
        <tr>
          <!-- loop through each value of the fields to get the table header -->
          <th v-for="field in fields" :key="field">
            {{ field }}
            <i class="bi bi-sort-alpha-down" aria-label="Sort Icon"></i>
          </th>
        </tr>
      </thead>
      <tbody>
        <!-- Loop through the list get the each form data -->
        <tr v-for="item in tableData" :key="item">
          <td>
            <RouterLink :to="'/szavazz/' + item.id">
              {{ item["id"] }}
            </RouterLink>
          </td>
          <td>
            <RouterLink :to="'/szavazz/' + item.id">
              {{ item["questions"] }}
            </RouterLink>
          </td>
        </tr>
      </tbody>
    </table>
    <table
      id="tableComponent"
      class="table table-bordered table-striped"
      v-if="isResult"
    >
      <thead>
        <tr>
          <!-- loop through each value of the fields to get the table header -->
          <th v-for="field in fields" :key="field">
            {{ field }}
            <i class="bi bi-sort-alpha-down" aria-label="Sort Icon"></i>
          </th>
        </tr>
      </thead>
      <tbody>
        <!-- Loop through the list get the each form data -->
        <tr v-for="item in tableData" :key="item">
          <td>
            <RouterLink :to="'/eredmenyek/' + item.id">
              {{ item["id"] }}
            </RouterLink>
          </td>
          <td>
            <RouterLink :to="'/eredmenyek/' + item.id">
              {{ item["questions"] }}
            </RouterLink>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
export default {
  name: "TableComponent",
  props: {
    //
    tableData: {
      type: Array,
    },
    fields: {
      type: Array,
    },
    isResult: {
      type: Boolean,
    },
  },
  data() {
    return {};
  },
  mounted() {},
};
</script>

<style scoped src="bootstrap/dist/css/bootstrap.min.css"></style>
