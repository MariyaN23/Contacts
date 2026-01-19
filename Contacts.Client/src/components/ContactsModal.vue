<script setup lang="ts">
import Dialog from 'primevue/dialog';
import Button from 'primevue/button';
import Message from 'primevue/message';
import DatePicker from 'primevue/datepicker';
import {Form, type FormSubmitEvent} from '@primevue/forms';
import {ref, watch} from "vue";
import {zodResolver} from '@primevue/forms/resolvers/zod';
import {z} from 'zod';
import {MAX_JOB_TITLE_LENGTH, MAX_NAME_LENGTH} from "@/utils/constants.ts";
import {dateToStringFormatter} from "@/utils/date-formatter.ts";
import {isValidPhoneNumber} from "libphonenumber-js";
import type {ContactRequest} from "@/types/ContactRequest.ts";
import type {ContactForm} from "@/types/ContactForm.ts";
import FormInput from "@/components/common/FormInput.vue";

const props = defineProps<{
  label?: string
  ariaLabel?: string
  icon: string
  size?: string
  variant?: string
  modalHeader: string
  initialValues: ContactForm
}>()
const emit = defineEmits<{
  (e: 'submit-form', values: ContactRequest): void
}>()
const visible = ref(false)
const birthDate = ref(props.initialValues.birthDate)
const formValues = ref<ContactForm>({
  name: props.initialValues.name,
  mobilePhone: props.initialValues.mobilePhone,
  jobTitle: props.initialValues.jobTitle,
  birthDate: birthDate.value,
})
const resolver = ref(zodResolver(
    z.object({
      name: z.string().trim()
          .min(1, {message: 'Name is required.'})
          .max(MAX_NAME_LENGTH, {message: `Name should be less than ${MAX_NAME_LENGTH}`}),
      mobilePhone: z.string().trim()
          .min(1, {message: 'Mobile phone is required.'})
          .refine((val) => isValidPhoneNumber(val), {message: 'Invalid phone number',}),
      jobTitle: z.string().trim()
          .min(1, {message: 'Job title is required.'})
          .max(MAX_JOB_TITLE_LENGTH, {message: `Name should be less than ${MAX_JOB_TITLE_LENGTH}`}),
      birthDate: z.date({message: 'Birthdate is required.'})
          .max(new Date(new Date().setHours(23, 59, 59, 999)),
              {message: 'Date cannot be in the future.'}),
    }),
))
const onFormSubmit = ({valid, values}: FormSubmitEvent) => {
  if (valid) {
    const payload: ContactRequest = {
      name: values.name,
      mobilePhone: values.mobilePhone,
      jobTitle: values.jobTitle,
      birthDate: dateToStringFormatter(values.birthDate),
    }
    emit('submit-form', payload)
    visible.value = false
  }
}
watch(() => props.initialValues, (newValues) => {
  formValues.value = {...newValues}
}, {deep: true})
</script>

<template>
  <div class="card flex justify-center">
    <Button
        :label="label"
        :aria-label="ariaLabel"
        :icon="icon"
        :size="size"
        :variant="variant"
        @click="visible = true"
    />
    <Dialog
        v-model:visible="visible"
        modal
        :header="modalHeader"
    >
      <Form
          v-slot="$form"
          v-if="visible"
          :resolver="resolver"
          :initialValues="formValues"
          @submit="onFormSubmit"
          class="flex flex-col gap-4 w-full md:w-72"
      >
        <FormInput
            name="name"
            label="Name"
            :invalid="$form.name?.invalid"
            :error-message="$form.name?.error?.message"
        />
        <FormInput
            name="mobilePhone"
            label="Mobile phone"
            :invalid="$form.mobilePhone?.invalid"
            :error-message="$form.mobilePhone?.error?.message"
        />
        <FormInput
            name="jobTitle"
            label="Job title"
            :invalid="$form.jobTitle?.invalid"
            :error-message="$form.jobTitle?.error?.message"
        />
        <div class="flex flex-col gap-1">
          <DatePicker name="birthDate" :model-value="birthDate" showIcon fluid/>
          <Message v-if="$form.birthDate?.invalid" severity="error" size="small" variant="simple">
            {{ $form.birthDate.error?.message }}
          </Message>
        </div>
        <Button type="submit" label="Submit"/>
      </Form>
    </Dialog>
  </div>
</template>
