<script setup>
import TheWelcome from "@/components/TheWelcome.vue";
import BarChart from "@/components/display.vue";
</script>

<template>
  <main>
    <TheWelcome />
    <BarChart />
  </main>
</template>
<script>
export default {
  components: {
    BarChart,
    TheWelcome,
  },
};
</script>
