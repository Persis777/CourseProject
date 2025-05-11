import React from "react";

export default function TaskPlan() {
  return (
    <div className="bg-gray-800  rounded-lg w-2/3">
      <h2 className="text-gray-300 font-semibold text-xl mb-2">Плани дій</h2>
      <div className="space-y-4">
        {[
          {
            id: 1,
            title: "Plan 1",
            steps: [
              { id: 1, description: "Step 1" },
              { id: 2, description: "Step 2" },
            ],
          },
        ].map((plan) => (
          <div
            key={plan.id}
            className="bg-gray-700 p-4 rounded-lg border border-gray-600"
          >
            <div className="flex justify-between items-center">
              <h3 className="text-gray-200 font-semibold text-lg">
                {plan.title}
              </h3>
              <button className="text-red-500 hover:text-red-600">
                Видалити
              </button>
            </div>

            <ul className="mt-4 space-y-2">
              {plan.steps.map((step) => (
                <li
                  key={step.id}
                  className="bg-gray-600 p-2 rounded-lg text-gray-200"
                >
                  {step.description}
                </li>
              ))}
            </ul>
            <button className="mt-4 text-blue-500 hover:text-blue-600">
              Додати крок
            </button>
          </div>
        ))}

        <button className="text-blue-500 hover:text-blue-600">
          Додати план
        </button>
      </div>
    </div>
  );
}
