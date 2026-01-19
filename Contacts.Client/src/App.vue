<script setup lang="ts">
import Contacts from "@/components/Contacts.vue";
import Toast from "primevue/toast";
import ConfirmDialog from 'primevue/confirmdialog';
</script>

<template>
  <Toast/>
  <ConfirmDialog></ConfirmDialog>
  <Contacts/>
</template>
