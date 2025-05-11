import React from "react";

interface Props {
  id: string;
  label: string;
  placeholder: string;
  // eslint-disable-next-line no-unused-vars
  onInput: (e: React.FormEvent<HTMLTextAreaElement>) => void;
  value: string;
}

export default function LITextArea({
  id,
  label,
  placeholder,
  onInput,
  value,
}: Props) {
  return (
    <div>
      <label
        htmlFor={id}
        className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
      >
        {label}
      </label>
      <textarea
        id={id}
        rows={4}
        onInput={(e: never) => onInput(e)}
        value={value}
        className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
        placeholder={placeholder}
      />
    </div>
  );
}
