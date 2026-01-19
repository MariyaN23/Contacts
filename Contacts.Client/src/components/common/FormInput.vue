<script setup lang="ts">
import InputText from "primevue/inputtext";
import FloatLabel from "primevue/floatlabel";
import Message from "primevue/message";

defineProps<{
  name: string
  label: string
  invalid?: boolean
  errorMessage?: string
}>()
</script>

<template>
  <div class="flex flex-col gap-1">
    <FloatLabel variant="in" :invalid="invalid">
      <InputText
          :id="name"
          :name="name"
          type="text"
          autocomplete="off"
          fluid
      />
      <label :for="name">
        {{ label }}
      </label>
    </FloatLabel>
    <Message
        v-if="invalid"
        severity="error"
        size="small"
        variant="simple"
    >
      {{ errorMessage }}
    </Message>
  </div>
</template>
