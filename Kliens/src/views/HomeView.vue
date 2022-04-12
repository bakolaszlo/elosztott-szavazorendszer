<script setup>
import TheWelcome from "@/components/TheWelcome.vue";
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
    TheWelcome,
  },
};
</script>
